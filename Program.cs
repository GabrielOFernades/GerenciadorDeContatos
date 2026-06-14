namespace GerenciadorDeContatos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ContatoRepository contatoRepository = new ContatoRepository();

            List<Contato> contatos = contatoRepository.CapturarDados();
        }
    }
}
