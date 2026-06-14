using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            LinhaLonga();

            Console.WriteLine($"O arquivo '{path}' não existe");
            Console.WriteLine("Deseja criar um arquivo novo com informações base?");
            Console.WriteLine("(Será baseado no arquivo contactsExample.json)");
            Console.WriteLine("[0] - Não \n[1] - Sim, criar arquivo");
            LinhaLonga();
        }
    }
}
