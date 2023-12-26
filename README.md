

# Desafio II do Curso CRM Dynamic 365

Este projeto tem como objetivo apresentar os conhecimentos adquiridos com as seguintes ferramentas:

- Plugins
- Actions
- WorkFlow Assembys
- Personalização da interface do usuário
- Desenvolvimento de processos de negócios personalizados.

Além dos Plugins de Conta e Contato apresentados no treinamento, foram incluídos os seguintes Plugins relacionados ao Aluno:

## Plugins

1. **PluginAssincPostOperation**
   - Na Entidade Aluno e Conta:
     - Efetua a validação do campo Telefone, que deverá ser obrigatório.

3. **PluginAlunoPreOperation**
   - Efetua a validação do campo Telefone e verifica se ele existe no cadastro de Instrutores, se exitir será efetuada uma associação onde o Aluno também é um instrutor em seguida apresentado informações na tela de Consulta Aluno.

4. **PluginAlunoPostOperation**
   - Na Entidade Aluno:
   -  Efetua a validação do campo Linkedin, que será obrigatório para a criação automática da Task em seguida os dados são apresentados na tela de consulta de Aluno

6. **PluginAssincPostOperation**
   -    - Na Entidade Aluno e Conta:
        - Executado após a gravação dos dados, onde será criada automaticamente a Formação Acadêmica do Aluno como Fundamental, os dados são apresentadados na tela de consulta de Aluno.

## Action

Na Entidade Aluno, foi criada a Action para Busca de CEP.


## WorkFlow

Na Entidade Aluno, foi criado um WorkFlow. Quando o Aluno é vinculado a um Curso e está em Curso será obrigatório informar data de Inicio e será criado automaticamente o Calendário de Aulas conforme a Duração do Curso.

## Regra de Negócio:**
- Quando o CEP existir no cadastro do Aluno, os demais campos de endereço se tornarão obrigatórios.
- Se estiver em Branco os campos serão apagados; 


