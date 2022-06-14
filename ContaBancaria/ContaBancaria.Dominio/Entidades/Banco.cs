namespace ContaBancaria.Dominio.Entidades
{
    public class Banco
    {
        private readonly string _descricao;
        private readonly ushort _numero;
        private readonly ushort _agencia;

        public Banco(string descricao, ushort numero, ushort agencia)
        {
            _descricao = descricao;
            _numero = numero;
            _agencia = agencia;
        }

        public override string ToString()
        {
            return $"{_descricao } { _numero } { _agencia }";
        }
    }
}
