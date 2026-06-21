using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GerenciadorDeContatos
{
    internal class ContatoService
    {


        public static bool ConferirCampos(string nome, string telefone)
        {
            bool nomeValido = ValidarNome(nome);
            bool telefoneValido = ValidarTelefone(telefone);

            if (nomeValido && telefoneValido)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ValidarNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                Console.WriteLine("O Nome deve ser preenchido");
                return false;
            }
            return true;
        }

        public static bool ValidarTelefone(string telefone)
        {
            Regex regNumeroCelular = new Regex(@"^\d+$");

            Match match = regNumeroCelular.Match(telefone);

            if (string.IsNullOrEmpty(telefone))
            {
                Console.WriteLine("O Telefone deve ser preenchido.");
                return false;
            }

            if (match.Success)
            {
                return true;
            }
            else
            {
                Console.WriteLine("O telefone não está no formato correto (somente números)");
                return false;
            }
        }

        public static bool ConferirCampos(Contato contato)
        {
            bool nomeValido = ValidarNome(contato.Nome);
            bool telefoneValido = ValidarTelefone(contato.Telefone);

            if (nomeValido && telefoneValido)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<Contato> SubstituirContato(ref List<Contato> listaBase, Contato contatoATrocar, int indice)
        {
            listaBase[indice] = contatoATrocar;
            return listaBase;
        }
    }
}
