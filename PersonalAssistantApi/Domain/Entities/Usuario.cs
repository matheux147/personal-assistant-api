namespace PersonalAssistantApi.Domain.Entities;

public class Usuario : Entity
{
    private string _nome = null!;

    private readonly List<Tarefa> _tarefas = new();
    private readonly List<Conta> _contas = new();

    public string Nome => _nome;

    public virtual IReadOnlyCollection<Tarefa> Tarefas => _tarefas.AsReadOnly();
    public virtual IReadOnlyCollection<Conta> Contas => _contas.AsReadOnly();

    protected Usuario() { }

    public Usuario(string nome)
    {
        SetNome(nome);
    }

    public void SetNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome inválido.");
        _nome = nome;
    }
}