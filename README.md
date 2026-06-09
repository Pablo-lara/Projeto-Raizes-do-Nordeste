#Projeto Raízes do Nordeste- API de Gerenciamento de Pedidos

O Projeto Raízes do Nordeste é uma API RESTful desenvolvida em .NET Core voltada para a automação e gerenciamento do fluxo de pedidos de uma lanchonete/restaurante
de forma omnichannel (atendendo pedidos via App, Balcão e Totem).
O sistema adota uma arquitetura em camadas bem definida, garantindo separação de responsabilidades, manutenibilidade e escalabilidade do código.

---

#1. Pré-requisitos e Tecnologias Utilizadas

Para executar este projeto localmente, você precisará ter instalado em sua máquina:
* SDK do .NET Core 8.0(ou superior)
* SQL Server(LocalDB ou Express)
* Postman ou Swagger(para testes de endpoints)

#Pacotes NuGet principais (Entity Framework):
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools

##Estrutura de pastas(Arquitetura em camadas):
ProjetoRaizes/
│
├── Controllers/            -Exposição dos Endpoints (Rotas HTTP, Validação do DTO)
│   ├── UsuarioController.cs
│   ├── CardapioController.cs
│   └── PedidosController.cs
│
├── Services/               -Camada de Negócio (Regras, validações e orquestração)
│   ├── UsuarioService.cs
│   ├── CardapioService.cs
│   └── PedidoService.cs
│
├── Repositories/           -Camada de Dados (Persistência, comandos e queries SQL)
│   ├── UsuarioRepository.cs
│   ├── CardapioRepository.cs
│   └── PedidoRepository.cs
│
├── Interfaces/             -Contratos de isolamento entre camadas (IoC)
│   ├── IUsuarioRepository.cs / IUsuarioService.cs
│   ├── ICardapioRepository.cs / ICardapioService.cs
│   └── IPedidoRepository.cs / IPedidoService.cs
│
├── Models/                 -Entidades de Domínio e Enums do Banco de Dados
│   ├── Usuario.cs
│   ├── Cardapio.cs
│   ├── Pedido.cs
│   └── ItemPedido.cs
│
├── DTOs/                   -Objetos de Transferência de Dados (Payloads de Entrada)
│   ├── RegistrarUsuarioDTO.cs
│   ├── CriarPedidoDTO.cs
│   └── ProcessarPagamentoDTO.cs
│
├── Data/                   -Contexto de Banco de Dados (EF Core DbContext)
│   └── AppDbContext.cs
│
├── appsettings.json           -Strings de Conexão com o SQL Server
└── Program.cs                 -Injeção de Dependências e Inicialização da API


##Como executar o projeto:
1-Clone o repositório:
git clone [https://github.com/seu-usuario/projeto-raizes.git](https://github.com/seu-usuario/projeto-raizes.git)
cd projeto-raizes/ProjetoRaizes

2-Configurar o Banco de dados:
Abra o arquivo appsettings.json e ajuste a string de conexão se necessário. Em seguida, aplique as Migrations para estruturar as tabelas no seu SQL Server:
dotnet ef database update - no terminal para aplicar as migrations.

3-Rodar a aplicação no terminal utilizando o comando:
dotnet run

A API iniciará localmente. Verifique o terminal para identificar a porta (Ex: http://localhost:5054). Acesse /swagger no navegador para visualizar a documentação interativa.

Obs: O arquivo Json do postman e as instruções para realização dos testes estão em um repositório separado, presente no link abaixo:
https://github.com/Pablo-lara/Projeto-Raizes-do-Nordeste---Colecao-Postman

-------

##Abaixo estão as documentações referentes aos endpoints presentes na API. Documentados conforme solicitado nas instruções da atividade:

/usuarios(cadastro, login,consentimento de dados)
#1 Endpoint: Cadatro de usuario
-Nome: Realizar o cadastro de um novo usuário no sistema com criptografia de senha e definição automática de metadados(id,data).
-Metodo Http + rota: POST /api/Usuario
-Autenticação e permissões: Pública (Nenhuma autenticação necessária).
-Parâmetros: * Path params: Não possui.
	Query params: Não possui.
-Body (request) JSON:
{
  "id": 0,
  "nome": "string",
  "sobrenome": "string",
  "email": "string",
  "senhaHash": "string",
  "dataCriacao": "2026-06-08T14:24:58.510Z",
  "permissaoDados": true,
  "dataPermissao": "2026-06-08T14:24:58.510Z"
}

-Response JSON:
{
  "nd": 1,
  "nome": "Pablo",
"sobrenome":"Lara",
  "email": "pablo@email.com",
"senhaHash":"string",
"dataCriacao": "2026-06-08T11:15:00Z",
  "permissaoDados": true,
  "dataPermissao": "2026-06-08T11:15:00Z"
}

-Codigos esperados:
200 - OK
21 - Created
400 - Requisicao invalida
-Padrao de erro unificado no final de /usuarios

#2 Endpoint: Autenticacao(Login)
-Nome: Autenticar um usuário cadastrado validando suas credenciais através do hash da senha.
-Método HTTP + rota: POST /api/Usuario/login
-Autenticação e permissões: Pública (Nenhuma autenticação necessária).
-Parâmetros: * Path params: Não possui.
	Query params: Não possui
-Body Request JSON:
{
  "email": "string",
  "senha": "string"
}

-Response JSON:
{
  "mensagem": "Login realizado com sucesso!",
  "usuarioId": 1,
  "nome": "Pablo Silva",
  "aceitouTermos": true
}
-Codigos esperados:
200 - OK
401 - Unauthorized - Email ou senha incorretos
-Padrao de erro no final do /usuarios

#3 Endpoint: Obter Termo de Consentimento
-Nome: Retornar o texto legal com as políticas de privacidade de dados para exibição do checkbox na interface.
-Método HTTP + rota: GET /api/Usuario/termo-consentimento
-Autenticação e permissões: Pública (Livre acesso para que o usuário possa ler os termos antes do cadastro/login).
-Parâmetros: * Path params: Não possui.
	Query params: Não possui.
-Body Request JSON:
	Não se aplica para requisições do tipo GET
-Response JSON:
{
  "texto": "Declaro que li e aceito os termos de uso e autorizo o processamento dos meus dados pessoais para fins acadêmicos no projeto Raízes."
}
-Codigos esperados:
200 - OK
400 - Bad Request - Tentar cadastro sem consentimento do uso de dados.
401 - Unauthorized - Senha ou email incorretos.
403 - Forbiden - Tentar acessar um endpoint protegido sem ter o consentimento aceito(desativado para facilitar testes)

-Padrao de erro unificado dos endpoints 1,2 e 3 abaixo:
Exemplo de erro geral JSON:
{
  "erro": "Descrição detalhada do motivo da falha para que o Front-end possa exibir.",
  "timestamp": "2026-06-08T11:15:00Z"
}
-Padrao de erro JSON:
{
  "erro": "É necessário consentir com o uso de dados para se cadastrar."
}
{
  "erro": "E-mail ou senha incorretos."
}
{
  "erro": "Acesso bloqueado. Aceite os termos de uso de dados primeiro."
}

----
/produtos(consulta)
#4 Endpoint: Exibicao cardapio
-Nome: Listar todos os itens e produtos ativos do cardápio para exibição no menu do usuário
-Método HTTP + rota: GET /api/Cardapio
-Autenticação e permissões: Pública (ou protegida sob a validação de consentimento de dados por meio do Action Filter [VerificarConsentimento]Obs:Desativado o filtro para facilitacao dos testes.
-Parâmetros: * Path params: Não possui.
	Query params: Não possui.
-Body Request JSON:
	Não se aplica para requisições do tipo GET
-Response JSON:
  {
    "id": 1,
    "nome": "Hambúrguer Raízes",
    "descricao": "Pão artesanal, blend de 150g, queijo prato e maionese da casa.",
    "preco": 28.9,
    "ativo": true
  },
  {
    "id": 2,
    "nome": "Batata Frita Rústica",
    "descricao": "Porção de batatas fritas temperadas com páprica e alecrim.",
    "preco": 15,
    "ativo": true
  },
  {
    "id": 3,
    "nome": "Suco Natural de Laranja",
    "descricao": "Copo de 400ml de suco de laranja puro, sem açúcar.",
    "preco": 9.5,
    "ativo": true
  },
  {
    "id": 4,
    "nome": "Cuscuz",
    "descricao": "Farinha de milho, molho de tomate, legumes, azeitona, ovos e sardinha",
    "preco": 17.5,
    "ativo": true
  }

-Codigos esperados:
200 - OK
403 - ACESSO BLOQUEADO, NECESSITA CONSENTIMENTO DO USO DE DADOS PARA ACESSAR O ENDPOINT.
-Padrao de erro JSON:
{
  "erro": "Acesso bloqueado. Aceite os termos de uso de dados primeiro.",
  "timestamp": "2026-06-08T13:54:00Z"
}


/produtos(pedido, finalizacao pedido)
#5 EndPoint: Realizar pedido
-Nome: Registrar uma nova solicitação de pedido no sistema, vinculando o usuário aos itens do cardápio escolhidos e calculando o valor total geral de forma automatizada no servidor.
-Método HTTP + rota: POST /api/Pedidos
-Autenticação e permissões: Protegida (Recomendado o uso do filtro [VerificarConsentimento] para garantir que o cliente só possa consumir recursos e realizar compras caso tenha aceitado as diretrizes
de privacidade de dados).
Obs.Filtro desativado para facilitar os testes
-Path params: Não possui.
	Query params: Não possui.
-Body Request JSON:
{
  "usuarioId": 1,
  "canal": 1, 
  "itens":
    {
      "itemCardapioId": 1,
      "quantidade": 2
    }
  ]
}
(Nota de mapeamento: 1 = App, 2 = Balcao, 3 = Totem).
-Resposta JSON:
{
  "id": 102,
  "usuarioId": 1,
  "dataPedido": "2026-06-09T08:50:00Z",
  "valorTotal": 57.80,
  "status": 1,
  "canal": 1,
  "itens": [...]
}-Codigos esperados:
200- OK
201 - Created
400 - Bad Request - Requisição malformada ou tentativa de pedir um item que não existe/está inativo no cardápio.
-Padrao de erro JSON:
{
  "erro": "Item 99 não encontrado no cardápio.",
  "timestamp": "2026-06-08T14:45:05Z"
}


/produtos(exibicao status, exibicao pedido pronto)
#6 EndPoint: Consultar Status do Pedido
-Nome: Permitir que o cliente acompanhe em tempo real o andamento e o estado atual do seu pedido específico.
-Método HTTP + rota: GET /api/Pedidos/{id}
-Autenticação e permissões: Protegida (Acesso ao cliente vinculado à conta ou portador do identificador).
-Path params: {id} (int) - Identificador numérico único do pedido a ser consultado.
	Query params: Não possui.
-Body Request JSON:
	Não se aplica para requisições do tipo GET.
-Response JSON:
{
  "pedidoId": 101,
  "statusAtual": "Recebido",
  "valorTotal": 72.80,
  "data": "2026-06-08T14:45:00Z"
}
-Codigos esperados:
200 - OK
404 - Not Found - pedido nao encontrado
-Padrao de erro JSON:
{
  "erro": "Pedido não encontrado.",
  "timestamp": "2026-06-08T14:47:00Z"
}


#7 EndPoint: Atualizar Status do pedido(fluxo da cozinha)
-Nome: Atualizar a etapa de progresso do pedido na cozinha, permitindo inclusive definir e sinalizar quando o prato estiver totalmente "Pronto" para retirada ou entrega.
-Método HTTP + rota: PUT /api/Pedidos/{id}/status
-Autenticação e permissões: Restrito (Uso exclusivo por usuários com papéis administrativos, gerenciais ou operadores da cozinha).
-Path params: {id} (int) - Código de identificação do pedido que sofrerá a alteração.
	Query params: Não possui.
-Body Request JSON:
{
  "novoStatus": 3
}
Nota técnica do projeto: O mapeamento do Enum utiliza inteiros sequenciais onde: 1 = Recebido, 2 = EmPreparacao, 3 = Pronto, 4 = Entregue.
-Response JSON:
{
  "mensagem": "Status do pedido alterado para Pronto com sucesso!"
}
-Codigos esperados:
 200 - OK
404 - Not Found - ID do pedido informado na rota não existe na base de dados.

-Padrao de erro JSON:
{
  "erro": "Pedido não encontrado.",
  "timestamp": "2026-06-08T15:02:11Z"
}


/pedidos/pagamento
#8 EndPoint: Processar Pagamento(simulado)
-Nome: Realizar a simulação financeira do pagamento de um pedido ativo e atualizar automaticamente seu estado para a etapa de preparação na cozinha.
-Método HTTP + rota: POST /api/pedidos/pagamento
-Autenticação e permissões: Protegida (Uso exclusivo pelo cliente logado dono do pedido)
-Path params: Não possui
	Query params: Não possui
-Body Request JSON:
{
  "pedidoId": 1,
  "formaPagamento": 1
}
(Nota de mapeamento: 1 = Credito, 2 = Debito, 3 = Dinheiro).
-Response JSON:
{
  "mensagem": "Pagamento de R$ 72,80 aprovado via Credito. O pedido está agora Em Preparação!"
}
-Codigos esperados:
200 - OK
400 - Bad Request - ID do pedido inválido ou tentativa de pagar um pedido que já foi processado/pago anteriormente.
-Padrao de erro JSON:
{
  "erro": "Não é possível pagar um pedido com o status atual: EmPreparacao.",
  "timestamp": "2026-06-09T08:35:00Z"
}

---

#Padrão de erros
Diante de falhas de validação (campos obrigatórios ausentes, formatos inválidos ou violações de regras de negócio), a API responderá com o status 400 Bad Request utilizando uma estrutura consistente:
{
  "erro": "Mensagem detalhada descrevendo a falha que ocorreu.",
  "timestamp": "2026-06-09T14:32:00Z"
}
