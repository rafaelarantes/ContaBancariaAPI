namespace ContaBancaria.Dominio.Entidades
{
    public class Usuario : Entity
    {
        public string Login { get; private set; }
        public string Senha { get; private set; }
        public string Autorizacao { get; private set; }

        public Usuario(string login, string senha, string autorizacao)
        {
            Login = login;
            Senha = senha;
            Autorizacao = autorizacao;
        }
    }
}
