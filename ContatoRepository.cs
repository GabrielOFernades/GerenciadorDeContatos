using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;

namespace GerenciadorDeContatos
{
    class ContatoRepository
    {
        private string ArquivoBase = Environment.CurrentDirectory + @"\contacts.json";

        public List<Contato> CapturarDados()
        {
            List<Contato> contatos = new List<Contato> { };
            if (!File.Exists(ArquivoBase))
            {
                Menu.OpcoesSeSemArquivo(ArquivoBase);
                CapturarDados();
                return contatos;
            }
            else
            {
                string conteudoJson = File.ReadAllText(ArquivoBase);

                if (string.IsNullOrWhiteSpace(conteudoJson))
                {
                    Console.WriteLine("Arquivo VAZIO.\nUtilizando lista vazia");
                }
                else
                {
                    contatos = JsonSerializer.Deserialize<List<Contato>>(conteudoJson);
                }
                return contatos;
            }
        }

        public List<Contato> AdicionarContato(List<Contato> contatos, int Id, string Nome, string Email, string Telefone)
        {
            Contato novoContato = new Contato(Id, Nome, Email, Telefone);
            contatos.Add(novoContato);
            return contatos;
        }

        public List<Contato> AdicionarContato(List<Contato> contatos, int Id, string Nome, string Telefone)
        {
            Contato novoContato = new Contato(Id, Nome, Telefone);
            contatos.Add(novoContato);
            return contatos;
        }

        public static List<Contato> RetornarListaDeContatosExemplo()
        {
            List<Contato> contatosExemplo = new List<Contato>
            {
                new Contato(1, "Gabriel", "gabrieldeoliveirafernandesdev@gmail.com", "998812341234"),
                new Contato(2,"Joao", "joaozinho@gmail.com", "122199994444")
            };

            return contatosExemplo;
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

        public static bool ConferirCampos(Contato contato)
        {
            var CamposDeTextoObrigatorios = new List<(string Valor, string Nome)>
            {
                (contato.Nome, "Nome" ),
                (contato.Telefone, "Telefone")
            };

            foreach (var campo in CamposDeTextoObrigatorios)
            {
                if (string.IsNullOrEmpty(campo.Valor))
                {
                    Console.WriteLine($"[AVISO] - O campo '{campo.Nome}' está vazio. Preencha novamente!");
                    Console.WriteLine("Pressione qualquer tecla para voltar...");
                    Console.ReadKey();
                    return false;
                }
            }

            return true;
        }

        public static bool IdDoContatoExiste(List<Contato> contatos, int id)
        {
            if (contatos.Any(contato => contato.Id == id))
            {
                return true;
            }
            return false;
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

        public static Contato EditarContato(Contato contato)
        {

            string nome;
            string telefone;
            string email;
            do
            {
                Menu.ImprimirInformacoesDoContato(contato);
                Console.WriteLine("\n");
                Console.Write("Novo Nome: ");
                nome = Console.ReadLine();

                Console.Write("Novo Telefone: ");
                telefone = Console.ReadLine();

                Console.Write("Novo Email: ");
                email = Console.ReadLine();

                contato.Nome = nome;
                contato.Telefone = telefone;
                contato.Email = email;

            } while (!ConferirCampos(contato));

            return contato;
        }

        public static List<Contato> SubstituirContato(ref List<Contato> listaBase, Contato contatoATrocar, int indice)
        {
            listaBase[indice] = contatoATrocar;
            return listaBase;
        }

        public static void CriarArquivoBase(string path, List<Contato> ContatosBase)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(ContatosBase, options);
            File.WriteAllText(path, json);

        }

        public static void CriarArquivoBase(string path)
        {
            File.WriteAllText(path, "");
        }

        public static void SalvarArquivo(string path, List<Contato> contatos)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(contatos, options);
            File.WriteAllText(path, json);
        }
    }
}
