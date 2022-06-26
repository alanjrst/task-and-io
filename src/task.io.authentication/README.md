
# Task.IO.Authentication
> Microsserviço responsável por gerenciar usuários e autenticação

## Índice
* [Informacoes gerais](#informacoes-gerais)
* [Tecnologias usadas](#tecnologias-usadas)
* [Bibliocatecas externas](#bibliotecas-externas)
* [Features](#features)
* [Arquitetura da solucao](#arquitetura-da-solucao)
* [Estrutura](#estrutura)
* [Configuracao](#configuracao)
* [Uso](#uso)
* [Banco de Dados](#banco-de-dados)
* [Status do projeto](#status-do-projeto)
* [Espaco para melhorias](#espaco-para-melhorias)
* [Contato](#contato)

## Informacoes gerais
- 

## Tecnologias usadas
- .NET Framework/SDK - 6.0
- Mysql

## Bibliocatecas externas
- Microsoft.EntityFrameworkCore - 5.0.10
- Microsoft.EntityFrameworkCore.Design - 5.0.50
- Pomelo.EntityFrameworkCore.MySql - 5.0.4
- BCrypt.Net-Core - 1.6.0
- Microsoft.IdentityModel.Tokens - 6.18.0
- System.IdentityModel.Tokens.Jwt - 6.18.0
- Swashbuckle.AspNetCore - 5.6.3

## Features
Liste os recursos prontos até o momento:
- Customers
- EventCustomers
- EventHeimdallHistories
 
## Arquitetura da solucao
![Arquitetura da solucao](./img/arquitetura-solucao.png)

## Estrutura

Camadas
```sh
├── 1.0 - Distribution  # Camada responsável por realizar a distribuição de entrada da solução
├── 2.0 - Application # Camada responsável por realizar a orquestração dos casos de uso
├── 3.0 - Domain # Camada responsável pra centralizar as regras de negócio
├── 4.0 - Integration # Camada responsável pelos projetos de integração externo
├── 5.0 - Infra # Camada responsável por centralizar features compartilhadas e pela base dados
├── 6.0 - Tests # Camada responsável pelos testes unitários
├── 7.0 - Tools # Camada responsável pelas ferramentas de debug
├── 8.0 - ObjectMothers # Camada responsável por montar os objetos a serem usados nos testes unitários
└── 9.0 - Solution Items # Camada responsável pra referenciar os arquivos que não estão contidos nas layers
```

Projetos
```sh
├── Account.Aurora.Interim.Worker  # Projeto responsável pelos serviços de eventos do Customer
├── Account.Aurora.Interim.Application # Projeto responsável pela orquestração dos eventos do Customer
├── Account.Aurora.Interim.Domain # Projeto responsável pelas regras de negócio do Customer
├── Account.Aurora.Interim.Integration.Bifrost # Projeto responsável pela integração do Customer com Promax utilizando Heimdall
├── Account.Aurora.Interim.Infra # Projeto responsável por compartilhar featureas comuns com os demais projetos, com exceção do dominio
├── Account.Aurora.Interim.Infra.Data # Projeto responsável do contexto da base de dados
├── Account.Aurora.Interim.Application.Tests # Projeto responsável pelos testes unitários do Application
├── Account.Aurora.Interim.Domain.Tests # Projeto responsável pelos testes unitários do Domain
├── Account.Aurora.Interim.Integration.Bifrost.Tests # Projeto responsável pelos testes do Integration.Bifrost
├── Account.Aurora.Interim.Worker.Tests # Projeto responsável pelos testes do Worker
├── Account.Aurora.Interim.Toll.Worker # Projeto responsável pelo envio de mensages de testes do Worker
├── Account.Aurora.Interim.Application.ObjectMother # Projeto responsável por manter os objetos de entrada do Application
├── Account.Aurora.Interim.Domain.ObjectMother # Projeto responsável por manter os objetos de entrada do Domain
├── Account.Aurora.Interim.Integration.Bifrost.ObjectMother # Projeto responsável por manter os objetos de entrada do Integration.Bifrost
└── Account.Aurora.Interim.Worker.ObjectMother # Projeto responsável por manter os objetos de entrada do Worker
```

## Configuracao
Para conseguir instalar o projeto é necessário baixar o repositorio na máquina local. Realizar restore do NUGET e rodar aplicação. 

## Uso
Para utilizar, deve rodar o serviço Worker. 

Em: 
```sh
├── 1.0 - Distribution  # Account.Aurora.Interim.Worker
```

Com isso aplicação estará rodando rodando no modo de desenvolvimento.

Para testar consumo, pode ser utilizado o Sample. Ferramenta criada, para envio de uma mensagem.

Em: 
```sh
├── 7.0 - Tools  # Account.Aurora.Interim.Toll.Worker
```

## Banco de Dados
![Coverage](./img/banco.png)

## Status do projeto
Projeto está: _em progresso_.

## Espaco para melhorias
- Migração para o .NET6.
- Banco de dados único;
- Utilização de cache;

## Contato
Criado por <timedecadastro@ambevtech.com.br> - Sinta-se livre para me contatar!

Criar migration

Acesse
task.io.authentication.InfraStructure
dotnet ef migrations add Initial --startup-project ../task.io.authentication.API