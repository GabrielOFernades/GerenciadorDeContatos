using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GerenciadorDeContatos
{
    internal class Menu
    {
        public static void LinhaLonga()
        {
            for(int i = 0; i < 50; i++)
            {
                Console.Write("=");
            }
            Console.Write("\n");
        }

        public static void OpcoesSeSemArquivo(string path)
        {

            Console.Clear();
            LinhaLonga();

            Console.WriteLine($"O arquivo '{path}' não existe");
            Console.WriteLine("Deseja criar um arquivo novo com informações base?");
            Console.WriteLine("[0] - Não. Sair \n[1] - Sim, criar arquivo\n[2] - Criar arquivo vazio");
            LinhaLonga();

            int opcao;
            Console.WriteLine("Digite o número: ");
            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                default:
                    Console.WriteLine("Opção Inválida! Tente novamente\n");
                    break;
                case 0:
                    Console.WriteLine("Fechando programa...");
                    Environment.Exit(0);
                    break;
                case 1:
                    ContatoRepository.CriarArquivoBase(path, ContatoRepository.RetornarListaDeContatosExemplo());
                    break;
                case 2:
                    ContatoRepository.CriarArquivoBase(path);
                    break;
                
            }

        }
        
        public static int MenuPrincipal(ref List<Contato> contatos)
        {
            int opcao;
            do
            {
                Console.Clear();
                LinhaLonga();
                Console.WriteLine("O que deseja fazer com os contatos?");
                Console.WriteLine("[0] - Sair\n[1] - Criar Contato");
                Console.WriteLine("[2] - Ler Contatos\n[3] - Atualizar Contato");
                Console.WriteLine("[4] - Deletar Contato");

                Console.Write("\nDigite a opção desejada: ");
                opcao = int.Parse(Console.ReadLine());
                switch (opcao)
                {
                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inserida é inválida.");
                        Console.ReadLine();
                        break;
                    case 0:
                        return 0;
                        break;
                    case 1:
                        AdicionarContatoMenu(ref contatos);
                        break;
                    case 2:
                        Console.Clear();
                        ImprimirListaDeContatos(contatos);
                        Console.WriteLine("Aperte qualquer tecla para voltar...");
                        Console.ReadKey();
                        break;
                    case 3:
                        MenuEditarContato(ref contatos);
                        break;
                }

            } while (opcao != 0);
            return 0;
        }

        public static void AdicionarContatoMenu(ref List<Contato> contatos)
        {
            string nome;
            string email;
            string telefone;
            int id = (contatos.Count + 1);

            bool camposEstaoCorretos = false;

            Contato novoContato = new Contato(0, "", "", "");

            do
            {
                Console.Clear();
                Console.WriteLine("=======ADICIONAR CONTATO=======");
                Console.WriteLine("> Digite o valor desejado <");

                Console.Write("Nome: ");
                nome = Console.ReadLine();

                Console.Write("\nEmail: ");
                email = Console.ReadLine();

                Console.Write("\nTelefone: ");
                telefone = Console.ReadLine();

                Console.WriteLine("\n");

                LinhaLonga();
                novoContato = new Contato(id, nome, email, telefone);
                camposEstaoCorretos = ContatoService.ConferirCampos(novoContato);
            } while (!camposEstaoCorretos);

            contatos.Add(novoContato);

        }

        public static void MenuEditarContato(ref List<Contato> contatos)
        {
            int idEscolhido;
            Contato contatoEditar = new Contato(0, "", "", "");

            do
            {
                Console.Clear();
                ImprimirListaDeContatos(contatos);
                Console.WriteLine("Digite o ID do contato: ");
                idEscolhido = int.Parse(Console.ReadLine());

                if(RetornarContatoPorId(contatos, idEscolhido) == null)
                {
                    Console.WriteLine("Contato inexistente na lista. Insira um ID válido");
                    Console.WriteLine("Pressione qualquer tecla para voltar...");
                    Console.ReadKey();
                }
                else
                {
                    contatoEditar = RetornarContatoPorId(contatos, idEscolhido);

                    contatoEditar = EditarContato(contatoEditar);

                    int indiceContato = contatos.FindIndex(contato => contato.Id == idEscolhido);

                    ContatoService.SubstituirContato(ref contatos, contatoEditar, indiceContato);

                    Console.Clear();
                    break;
                }

            } while (RetornarContatoPorId(contatos, idEscolhido) == null || !ContatoService.ConferirCampos(contatoEditar));

        }

        public static void ImprimirInformacoesDoContato(Contato contato)
        {
            Console.WriteLine($"====Informações do contato '{contato.Id}'===");
            Console.WriteLine($"Nome: '{contato.Nome}'");
            Console.WriteLine($"Email: '{contato.Email}'");
            Console.WriteLine($"Telefone: '{contato.Telefone}'");
        }
         public static void ImprimirListaDeContatos(List<Contato> contatos)
        {

            Console.WriteLine("=======LISTA DE CONTATOS=======");
            foreach (Contato contato in contatos)
            {
                Console.WriteLine("Id: " + contato.Id);
                Console.WriteLine("Nome: " + contato.Nome);
                Console.WriteLine("Telefone: " + contato.Telefone);
                Console.WriteLine("Email: " + contato.Email);
                Menu.LinhaLonga();
            }
        }

        public static Contato EditarContato(Contato contato)
        {

            string nome;
            string telefone;
            string email;
            bool camposValidos = false;
            while (camposValidos == false)
            {
                Menu.ImprimirInformacoesDoContato(contato);
                Console.WriteLine("\n");
                Console.Write("Novo Nome: ");
                nome = Console.ReadLine();

                Console.Write("Novo Email: ");
                email = Console.ReadLine();

                Console.Write("Novo Telefone: ");
                telefone = Console.ReadLine();


                camposValidos = ContatoService.ConferirCampos(nome, telefone);

                if (camposValidos)
                {
                    contato.Nome = nome;
                    contato.Telefone = telefone;
                    contato.Email = email;
                }
                else
                {
                    camposValidos = false;
                }

            };

            return contato;
        }

        public static Contato RetornarContatoPorId(List<Contato> contatos, int id)
        {
            if (IdDoContatoExiste(contatos, id))
            {
                Contato contato = contatos.FirstOrDefault(contato => contato.Id == id);
                return contato;
            }
            return null;
        }

        public static bool IdDoContatoExiste(List<Contato> contatos, int id)
        {
            if (contatos.Any(contato => contato.Id == id))
            {
                return true;
            }
            return false;
        }
    }
}
