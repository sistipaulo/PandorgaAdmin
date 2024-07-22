# Sistema Escolar com Site Institucional

Este projeto é um sistema escolar completo com um site institucional desenvolvido em ASP.NET Core e .NET 6. O sistema é projetado para gerenciar informações de alunos, professores, turmas, eventos e salas, além de fornecer um site institucional para a escola.

![image](https://github.com/user-attachments/assets/d32c0133-ad7a-4fae-b867-85d4b1b6f700)

![image](https://github.com/user-attachments/assets/121e4a88-d729-4120-969f-be20eec51401)


## Funcionalidades

- **Gestão de Alunos**: Cadastro e gerenciamento de informações dos alunos, incluindo dados pessoais, contato e turma.
- **Gestão de Professores**: Cadastro e gerenciamento de informações dos professores, incluindo dados pessoais, cargo e turmas associadas.
- **Gestão de Turmas**: Cadastro e gerenciamento de turmas, associando professores e salas.
- **Gestão de Salas**: Cadastro e gerenciamento de salas de aula, incluindo capacidade e turmas associadas.
- **Gestão de Eventos**: Cadastro e gerenciamento de eventos escolares, associando turmas e salas.
- **Site Institucional**: Páginas públicas para apresentar informações sobre a escola, eventos futuros e outras informações relevantes.

## Tecnologias Utilizadas

- **ASP.NET Core 6**: Framework para desenvolvimento web.
- **Entity Framework Core**: ORM para interação com o banco de dados.
- **JavaScript**: Para interatividade no frontend.
- **HTML/CSS**: Para o design das páginas web.

## Requisitos

- **.NET 6 SDK**: [Download e Instalação](https://dotnet.microsoft.com/download/dotnet/6.0)
- **Visual Studio 2022 ou superior**: IDE recomendada para desenvolvimento.

## Configuração

### Clonar o Repositório

```bash
git clone https://github.com/sistipaulo/PandorgaAdmin.git
cd seu-repositorio
```

## Configurar o Banco de Dados

- **Criar o Banco de Dados**: Configure a string de conexão no arquivo appsettings.json para apontar para o seu banco de dados.
```bash
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SeuBancoDeDados;Trusted_Connection=True;"
}
```
- **Executar as Migrations**: No terminal, execute os comandos para criar o banco de dados e aplicar as migrations.
```bash
update-database
```
## Estrutura do Projeto
- **Controllers**: Contém os controladores MVC para gerenciar as solicitações e respostas.
- **Models**: Contém os modelos de dados, incluindo classes como Aluno, Professor, Turma, Sala, e Evento.
- **Views**: Contém as views Razor para renderizar as páginas HTML.
- **wwwroot**: Contém arquivos estáticos como CSS, JavaScript e imagens.

## Contribuição
Se você deseja contribuir para o projeto, por favor, siga estas etapas:

1. Fork o repositório.
2. Crie uma branch para suas alterações (git checkout -b minha-alteracao).
3. aça suas alterações e commit com uma mensagem clara (git commit -am 'Adiciona nova funcionalidade').
4. Push para a branch (git push origin minha-alteracao).
5. Crie um pull request para revisão.
