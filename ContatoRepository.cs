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
        private string filePath = "contacts.json";

        public List<Contato> CapturarDados()
        {
            if (!File.Exists(filePath))
            {
                Menu.OpcoesSeSemArquivo(filePath);
                return null;
            }
            else
            {
                string conteudoJson = File.ReadAllText(filePath);
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
    }
}
