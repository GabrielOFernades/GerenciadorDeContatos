using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace GerenciadorDeContatos
{
    class ContatoRepository
    {
        private string ArquivoBase = Environment.CurrentDirectory + @"\contacts.json";

        public List<Contato> CapturarDados()
        {
            if (!File.Exists(ArquivoBase))
            {
                Menu.OpcoesSeSemArquivo(ArquivoBase);
                return null;
            }
            else
            {
                string conteudoJson = File.ReadAllText(ArquivoBase);
                List<Contato>? contatos = JsonSerializer.Deserialize<List<Contato>>(conteudoJson);

                if (contatos == null || contatos.Count == 0)
                {
                    Console.WriteLine("Nenhum contato no arquivo.");
                }

                return contatos;
            }
        }

        public List<Contato> AdicionarContato(List<Contato> contatos, string Nome, string Email, string Telefone)
        {
            Contato novoContato = new Contato(Nome, Email, Telefone);
            contatos.Add(novoContato);
            return contatos;
        }

        public static List<Contato> RetornarListaDeContatosExemplo()
        {
            List<Contato> contatosExemplo = new List<Contato>
            {
                new Contato("Gabriel", "gabrieldeoliveirafernandesdev@gmail.com", "998812341234"),
                new Contato("Joao", "joaozinho@gmail.com", "122199994444")
            };

            return contatosExemplo;
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
