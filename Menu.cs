using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        
        public static int MenuPrincipal(List<Contato> contatos)
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
                        
                        break;
                    case 2:
                        ImprimirLista(contatos);
                        break;
                }

            } while (opcao != 0);
            return 0;
        }

        public static void ImprimirLista(List<Contato> contatos)
        {
            Console.WriteLine("=======LISTA DE CONTATOS=======");
            foreach(Contato contato in contatos)
            {
                Console.WriteLine("Nome: " + contato.Nome);
                Console.WriteLine("Telefone: " + contato.Telefone);
                Console.WriteLine("Email: " + contato.Email);
                LinhaLonga();
            }
            Console.WriteLine("Aperte qualquer tecla para voltar...");
            Console.Read();
        }

    }
}
