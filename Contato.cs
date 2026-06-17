using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GerenciadorDeContatos
{
    internal class Contato
    {
        //definição dos atributos
        private int id;
        private string nome;
        private string email;
        private string telefone;

        //get e set dos atributos

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
            }
        }
        public string Nome {
            get { return nome; } 
            set 
            {
                if(string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Nome não deve estar vazio.");
                }
                else
                {
                    nome = value;
                }
            } 
        }
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
            }
        }
        public string Telefone 
        { 
            get { return telefone; } 
            set
            {
                if(string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Telefone não deve estar vazio");
                }
                else
                {
                    telefone = value;
                }
            } 
        }

        //construtor 1
        [JsonConstructor]
        public Contato(int id, string nome, string email, string telefone)
        {
            this.id = id;
            this.nome = nome;
            this.email = email;
            this.telefone = telefone;
        }

        //construtor 2 (sem email)
        public Contato(int id, string nome, string telefone)
        {
            this.id = id;
            this.nome = nome;
            this.telefone = telefone;
        }
    }
}
