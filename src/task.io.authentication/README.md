
# Task.IO.Authentication
> Microsserviço responsável por gerenciar usuários e autenticação

## Índice
* [Informacoes gerais](#informacoes-gerais)
* [Tecnologias usadas](#tecnologias-usadas)
* [Bibliocatecas externas](#bibliotecas-externas)
* [Features](#features)
* [Banco de Dados](#banco-de-dados)

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

## Bando de dados

Como gerar uma nova migration

```
cd task.io.authentication.InfraStructure
dotnet ef migrations add Initial --startup-project ../task.io.authentication.API
```