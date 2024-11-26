# Project Manager Api

## Visão Geral

O `ProjectManagerApi` é uma aplicação desenvolvida em .NET 8 com C# 12.0 que gerencia projetos e tarefas. A solução é composta por várias camadas, incluindo domínio, infraestrutura, aplicação e testes. Foram utilizado as seguintes libs: MediatR, Entity Framework Core, MOQ e xunit. Como base de dados foi utilizado o PostgreSQL

## Execução do projeto

Ao baixar o repositório em sua máquina e abrir o Terminal na raiz do projeto, com o Docker já em execução, rodar o seguinte comando:
```
docker network create pm-network

docker compose up
 ```
 o arquivo do docker compose irá executar um script de inserção de usuário, caso exclua a imagem ou container do banco terá que excluir o volume gerado na execução para funcionar corretamente.
 
 **Id 1 usuário gerente**
 
 **Id 2 usuário analista**

## Estrutura do Projeto

A solução está organizada da seguinte forma:

### ProjectManagerApi.Domain

Contém as entidades de domínio, enums e outras classes relacionadas ao domínio da aplicação.

- **Models**
  - **DataModels**
    - `Task.cs`: Representa uma tarefa com propriedades como Id, Título, Descrição, Prazo de Conclusão, Última Atualização, Status, Prioridade, ProjetoId, Projeto, Comentários e Históricos de Tarefas.
    - `User.cs`: Representa um usuário com propriedades como Id, Nome, Posição, Projetos e Comentários.
    - `TaskHistory.cs`: Representa o histórico de uma tarefa com propriedades como Id, Item Modificado, Valores Antigos, Valores Novos, Data de Modificação e TaskId.
    - `Comment.cs`: Representa um comentário com propriedades como Id, Texto do Comentário, Data de Criação, TaskId e UserId.
    - `Projeto.cs`: Representa um projeto com propriedades como Id, Nome, Ativo, UserId e Tarefas.
  - **Enums**
    - `PriorityEnum.cs`: Enum para definir a prioridade de uma tarefa (Alta, Média, Baixa).
    - `StatusEnum.cs`: Enum para definir o status de uma tarefa (Pendente, Em Andamento, Concluída).

### ProjectManagerApi.Infrastructure

Contém a configuração do contexto de dados e repositórios.

- **DataContextConfigurations**
  - `AppDbContext.cs`: Configura o contexto do banco de dados com DbSets para Projetos, Usuários, Tarefas, Comentários e Históricos de Tarefas.
- **Repositories**
  - `ProjectRepository.cs`: Implementa métodos para manipulação de projetos no banco de dados.

### ProjectManagerApi.Application

Contém os handlers e comandos para a aplicação.

- **Handlers**
  - **Tasks**
    - **Commands**
      - **Create**
        - `AddTasksByProjectIdCommand.cs`: Comando para adicionar uma tarefa a um projeto específico.
        - `AddTasksByProjectIdHandler.cs`: Handler para processar o comando de adicionar uma tarefa a um projeto específico.

### ProjectManagerApi.Tests

Contém os testes unitários para a aplicação.

- **Repositories**
  - `ProjectTests.cs`: Testes para o repositório de projetos, incluindo métodos para obter todos os projetos, obter um projeto por Id, criar um projeto e desativar um projeto.
- **Handlers**
  - `AddTasksByProjectIdHandlerTests.cs`: Testes para o handler de adicionar tarefas por Id do projeto, incluindo cenários onde o projeto não é encontrado, o limite de tarefas é excedido e a criação de uma tarefa com sucesso.

## Endpoints da API

### Projetos

- **GET /projeto**
  - Descrição: Retorna todos os projetos.
  - Método HTTP: `GET`

- **POST /projeto**
  - Descrição: Cria um novo projeto para o usuário logado.
  - Método HTTP: `POST`

- **DELETE /projeto**
  - Descrição: Deleta um projeto do usuário logado.
  - Método HTTP: `DELETE`

### Relatórios

- **GET /relatorio**
  - Descrição: Retorna os dados do relatório de Performance dos usuários.
  - Método HTTP: `GET`

### Tarefas

- **GET /tarefa**
  - Descrição: Lista as tarefas de um projeto.
  - Método HTTP: `GET`

- **POST /tarefa**
  - Descrição: Cria uma tarefa para um projeto.
  - Método HTTP: `POST`

- **PATCH /tarefa**
  - Descrição: Atualiza algumas informações da tarefa pelo Id.
  - Método HTTP: `PATCH`

- **DELETE /tarefa**
  - Descrição: Exclui a tarefa pelo Id.
  - Método HTTP: `DELETE`

### Comentários

- **POST /tarefa/comentario**
  - Descrição: Cria um novo comentário na referida tarefa.
  - Método HTTP: `POST`

## Refinamento

### Minhas Peguntas ao P.O são:

- **Quais métricas específicas além do número médio de tarefas concluídas por usuário nos últimos 30 dias são importantes para os relatórios de desempenho? Devemos incluir gráficos ou visualizações específicas?**

- **Devemos permitir que os gerentes ou administradores aumentem o limite de tarefas por projeto em casos excepcionais? Se sim, como isso deve ser controlado e registrado?**

- **Como devemos tratar a situação em que a prioridade de uma tarefa foi atribuída incorretamente? Existe algum processo de exceção para permitir a alteração da prioridade?**
 
- **Devemos permitir a remoção em massa de tarefas pendentes para facilitar a exclusão do projeto?**

## Comentários com minhas indicações para a evolução do projeto

***FluentValidation:*** Implementar a biblioteca FluentValidation para validar as entradas do usuário de forma declarativa e consistente.

***Middleware de Tratamento de Erros:*** Criar um middleware para tratamento global de erros, garantindo que todas as exceções sejam capturadas e tratadas de forma consistente.

***Autenticação e Autorização:*** Implementar autenticação e autorização robustas 

***Análise Estática de Código:*** Utilizar ferramentas de análise estática de código, como SonarQube, para identificar e corrigir problemas de qualidade de código.

***Logging:*** Implementar um sistema de logging robusto utilizando bibliotecas como Serilog ou NLog para rastrear e diagnosticar problemas em produção.

***Documentação de API:*** Utilizar Swagger para documentar a API de forma interativa e facilitar o consumo por outros desenvolvedores.