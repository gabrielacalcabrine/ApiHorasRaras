Loucademy Horas Raro
=================
#### O presente projeto permite a melhor organização e otimização das atividades para obtenção do escopo almejado com excelência. Por meio do cadastro de projetos, tarefas e usuarios, é possível obter uma melhor organização e otimização da sua produção. Aqui você obtem informações gerais sobre como utilizar a Api Loucademy.Horas.Raro, como autenticar o usuario, os filtros que pode utilizar, verificar a estruturação da resposta, visualização das fitures e endpoints da Api.

---
# Sumário
   * [Sobre](#Loucademy-horas-raro)
   * [Tabela de Conteudo](#tabela-de-conteudo)
   * [Como usar](#como-usar)
   * [Endpoints](#endpoints)
   * [Integração com Toggl](#integracao-com-toggl)
   * [Tecnologias](#tecnologias)
   * [Features](#features)
---
# Como usar

## Pré-requisitos

Para iniciar, instale as seguintes ferramentas na sua máquina:
[Git](https://git-scm.com), [DotNet 6](https://dotnet.microsoft.com/en-us/). 
Importante ter um editor [Visual Studio](https://visualstudio.microsoft.com/pt-br/downloads/) ou [VS code](https://code.visualstudio.com/)

## Instalação
```bash
# Clone este repositório
$ git clone <https://gitlab.com/gabrielacalcabrine/loucademy_horas_raro>

# Acesse a pasta por meio do visualStudio
$ Abra a solution Loucademy.Horas.Raro

# Execute o projeto
$ Abra a solution Loucademy.Horas.Raro

# O servidor inciará na porta:7162 - acesse <https://localhost:7162/swagger/index.html>
```
---
# Endpoints:

### Usuário:
- [POST] /api/usuarios - Cadastra usuário

- [GET] /api/usuarios - Busca lista de usuarios

- [GET] /api/usuarios/{id} - Busca um unico usuario

- [PUT] /api/usuarios/{id} - Altera usuario

- [DELETE] /api/usuarios/{id} - Deleta usuario

- [PATCH] /api/usuarios/{id} - Edita Role de usuario

- [PATCH] /api/usuarios/{id} - Edita Senha de usuario

### Auth:
- [POST] /api/auth/login - Loga usuario

- [POST] /api/auth/esqueci-senha - Envia email contendo codigo para alterar senha

- [PUT] /api/auth/altera-senha - Altera senha do usuario

### Tarefa:
- [POST] /api/tarefas - Cria nova tarefa

- [GET] /api/tarefas - Lista todas as tarefas

- [GET] /api/tarefas{id} - Lista tarefas por Usuario

- [GET] /api/tarefas{id} - Lista tarefas por Projeto

- [GET] /api/tarefas/{id} - Busca unica tarefa

- [PUT] /api/tarefas/{id} - Edita tarefa

- [PATCH] /api/tarefas/{id} - Interrompe inicio de tarefa

- [DELETE] /api/tarefas/{id} - Deleta tarefa

### Projeto:
- [POST] /api/projetos - Cria novo projeto

- [GET] /api/projetos - Lista projetos

- [GET] /api/projetos/{id} - Busca unico projeto

- [PUT] /api/projetos/{id} - Edita projeto

- [PATCH] /api/projetos/{id} - Atribui novos usuários a projeto

- [PATCH] /api/projetos/{id} - Remove usuário de projeto

### Relatórios:
- [GET] /api/relatorios - Busca do dia tarefas da pessoa logada

- [GET] /api/relatorios/semana - Busca tarefas da semana da pessoa logada

- [GET] /api/relatorios/mes - Busca tarefas do mes da pessoa logada

- [GET] /api/relatorios/admin/{userId} -  Busca ultimas tarefas do usuario informado
- [GET] /api/relatorios/admin/{userId}/semana - Busca tarefas da semana da pessoa logada
- [GET] /api/relatorios/admin/{userId}/mes - Busca tarefas do mes da pessoa logada
 
---
##  Integração com Toggl:

O Horas Raro realiza integração com [Toggl](https://toggl.com) para recuperar atividades de um colaborador que já possiu atividades cadastradas. Exemplo de como consumir a API:

`workspace_id` : **Obrigatório** . O Id dos dados você deseja acessar.

 `since`:  formato de data ISO 8601 (AAAA-MM-DD).

 `until`:  formato de data ISO 8601 (AAAA-MM-DD). 

 `user_agent`: **Obrigatório** . api_test em caso de acesso com token

## Resposta de Sucesso

Estrutura geral de respostas bem sucedidas
```json
 {
    "id": 5,
    "nomeProjeto": null,
    "data": null,
    "horaInicio": null,
    "horaFim": null,
    "totalHoras": null,
    "podeReajuste": false
  }
```
## Solicitações negada
Estrutura geral de resposta em caso de acesso negado
```json
{
  "codigo": 401,
  "descricao": null,
  "mensagens": [
    "Acesso negado"
  ],
  "detalhe": null
}
```
---
# Features
- Integração com Toggl
- Gerência de projetos
- Gestão de horas trabalhadas

### Funcionalidades futuras
- Integração com Tangerino
- Integração com Ponto Mais

---
# 🛠 Tecnologias

As seguintes ferramentas foram usadas na construção do projeto:

- [DotNet 6](https://dotnet.microsoft.com/en-us/)
- [Visual Studio](https://visualstudio.microsoft.com/pt-br/downloads/)
- [Vs code](https://code.visualstudio.com/)
- [DBeaver](https://dbeaver.io/download/)

---
# Autores
 <sub><b>Rômulo Simiquel, Gabriela Calcabrine</b></sub></a> <a href="https://blog.rocketseat.com.br/author/thiago//" title="Rocketseat">🚀</a>


Feito com ❤️ por Rômulo Simiquel, Gabriela Calcabrine 👋🏽 Entre em contato!

[![Gmail Badge](https://img.shields.io/badge/-loucademy@gmail.com-c14438?style=flat-square&logo=Gmail&logoColor=white&link=mailto:tgmarinho@gmail.com)](mailto:loucademy@gmail.com)
