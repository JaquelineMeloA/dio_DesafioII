using System;
using System.Collections.Generic;
using System.IdentityModel.Metadata;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using System.Windows.Controls;
using System.Windows;
using System.Xml.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Newtonsoft.Json.Linq;
using System.DirectoryServices.ActiveDirectory;

namespace PluginsTreinamento
{
    public class PluginAlunoPreOperation : IPlugin
    {
        // método requerido para execução do Plugin recebendo como parâmetro os dados do provedor de serviço
        public void Execute(IServiceProvider serviceProvider)
        {
            // Variável contendo o contexto da execução
            IPluginExecutionContext context =
                (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            // Variável contendo o Service Factory da Organização
            IOrganizationServiceFactory serviceFactory =
                (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));

            // Variável contendo o Service Admin que estabele os serviços de conexão com o Dataverse
            IOrganizationService serviceAdmin = serviceFactory.CreateOrganizationService(null);

            // Variável do Trace que armazena informações de LOG
            ITracingService trace = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            // Verifica se contém dados para o destino e se corresponde a uma Entity
            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
            {
                // Variável do tipo Entity herdando a entidade do contexto
                Entity entidadeContexto = (Entity)context.InputParameters["Target"];

                if (entidadeContexto.LogicalName == "dio_aluno") // verifica se a entidade do contexto é account
                {
                   

                    if (entidadeContexto.Attributes.Contains("dio_telefone")) // verifica se contém o atributo telephone1
                    {
                        // variável para herdar o conteúdo do atributo telephone1 do contexto
                        var phone1 = entidadeContexto["dio_telefone"].ToString();
                        if (String.IsNullOrEmpty(phone1))
                        {
                            throw new InvalidPluginExecutionException("Campo Telefone principal é obrigatório!!"); // exibe Exception de Erro
                        }
                        // variável string contendo  FetchXML para consulta do contato						
                        string FetchInstrutores = @"<?xml version='1.0'?>" +
                        "<fetch distinct='false' mapping='logical' output-format='xml-platform' version='1.0'>" +
                        "<entity name='dio_instrutores'>" +
                        "<attribute name='dio_instrutoresid'/>" +
                        "<attribute name='dio_name'/>" +
                        "<filter type='and'>" +
                        "<condition attribute='dio_telefone' value='" + phone1 + "' operator='eq'/>" +
                        "</filter>" +
                        "</entity>" +
                        "</fetch>";

                        trace.Trace("FetchInstrutores: " + FetchInstrutores); // armazena informações de LOG


                        // variável contendo o retorno da consulta FetchXML
                        var primarycontact = serviceAdmin.RetrieveMultiple(new FetchExpression(FetchInstrutores));
                        trace.Trace("Total: " + primarycontact.Entities.Count);
                        if (primarycontact.Entities.Count > 0) // verifica se contém entidade
                        {
                            trace.Trace("Total: " + primarycontact.Entities.Count);
                            // para cada entidade retornada atribui a variável entityContact
                            foreach (var entityInstrutores in primarycontact.Entities)
                            {
                                trace.Trace("dio_instrutores: " + entityInstrutores.Id);
                                // atribui referência de entidade para o atributo primarycontactid (Contato primário)
                                entidadeContexto["dio_alunoinstrutor"] = new EntityReference("dio_instrutores", entityInstrutores.Id);

                            }

                        }
                        else
                        {
                            entidadeContexto["dio_alunoinstrutor"] = null;
                        }


                    }


                }
            }

        }
    }

}
