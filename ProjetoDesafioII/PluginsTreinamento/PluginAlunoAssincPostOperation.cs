using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace PluginsTreinamento
{
    public class PluginAlunoAssincPostOperation : IPlugin
    {
		// método requerido para execução do Plugin recebendo como parâmetro os dados do provedor de serviço
		public void Execute(IServiceProvider serviceProvider)
		{
			try //tentativa de execução
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
                   
		
                     for (int i = 0; i < 10; i++)
					{

						// Variável para nova entidade Formacao Academica vazia
						var formacaoAcademica = new Entity("dio_formacaoacademica");

						// atribuição dos atributos para novo registro da entidade Formacao Academica

						formacaoAcademica.Attributes["dio_name"] = $"Registro Formação Assinc vinculado ao Aluno";
						formacaoAcademica.Attributes["dio_tipodeformacao"] = "Ensino Médio";
						formacaoAcademica.Attributes["dio_curso"] = "Ensino Fundamental";
			   			formacaoAcademica.Attributes["dio_aluno"] = new EntityReference("dio_aluno", context.PrimaryEntityId);
						formacaoAcademica.Attributes["createdby"] = new EntityReference("systemuser", context.UserId);
						trace.Trace("dio_name: " + formacaoAcademica.Attributes["dio_name"].ToString());

						serviceAdmin.Create(formacaoAcademica); // executa o método Create para entidade  Formacao Academica

                 
                    }

                }
			}
			catch (InvalidPluginExecutionException ex) // em caso de falha
			{
				throw new InvalidPluginExecutionException("Erro ocorrido: " + ex.Message); // exibe a Exception
			}
		}
	}
}
