namespace PersonalAssistantApi.Domain.Entities;

public class Conta : Entity
{
    private readonly Guid _usuarioId;
    private string _descricao = null!;
    private decimal _valor;
    private readonly DateTime _dataVencimento;
    private bool _pago;

    public Guid UsuarioId => _usuarioId;
    public string Descricao => _descricao;
    public decimal Valor => _valor;
    public DateTime DataVencimento => _dataVencimento;
    public bool Pago => _pago;

    public virtual Usuario? Usuario { get; private set; }

    protected Conta() { }

    public Conta(Guid usuarioId, string descricao, decimal valor, DateTime dataVencimento)
    {
        if (usuarioId == Guid.Empty) throw new ArgumentException("UsuarioId inválido.");
        if (valor <= 0) throw new ArgumentException("Valor deve ser maior que zero.");

        _usuarioId = usuarioId;
        _descricao = descricao;
        _valor = valor;
        _dataVencimento = dataVencimento;
        _pago = false;
    }

    public void Pagar() => _pago = true;
}