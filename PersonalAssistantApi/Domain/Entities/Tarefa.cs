namespace PersonalAssistantApi.Domain.Entities;

public class Tarefa : Entity
{
    private readonly Guid _usuarioId;
    private readonly string _titulo = null!;
    private readonly DateTime _data;
    private bool _concluida;

    public Guid UsuarioId => _usuarioId;
    public string Titulo => _titulo;
    public DateTime Data => _data;
    public bool Concluida => _concluida;

    public virtual Usuario? Usuario { get; private set; }

    protected Tarefa() { }

    public Tarefa(Guid usuarioId, string titulo, DateTime data)
    {
        if (usuarioId == Guid.Empty) throw new ArgumentException("UsuarioId inválido.");
        if (string.IsNullOrWhiteSpace(titulo)) throw new ArgumentException("Título é obrigatório.");

        _usuarioId = usuarioId;
        _titulo = titulo;
        _data = data;
        _concluida = false;
    }

    public void Concluir() => _concluida = true;
}