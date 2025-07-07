namespace Dominio.Interfaces;

using Dominio.Models;

public interface IOrcamento
{
    void CadastrarOrcamento(Orcamento orcamento);
    void EditarOrcamento(Orcamento orcamento);
    void ExcluirOrcamento(int id);
    abstract Orcamento ObterPorId(int id);
    abstract Orcamento ObterPorNumeroOrcamento(int numeroOrcamento);
    abstract Orcamento ObterPorDemanda(int idDemanda);
    abstract Orcamento ObterPorCliente(int idCliente);
    abstract Orcamento ObterPorDataDeAbertura(DateTime dataDeAbertura);

}
