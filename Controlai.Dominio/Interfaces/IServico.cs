namespace Dominio.Interfaces;

using Dominio.Models;

public interface IServico
{
    public void CadastrarServico(Servico servico);
    public void EditarServico(Servico servico);
    public void ExcluirServico(int id);
    public Servico ObterPorId(int id);
    public Servico ObterPorNome(string nome);
}
