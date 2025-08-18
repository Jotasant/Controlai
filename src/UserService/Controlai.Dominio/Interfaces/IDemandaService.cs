namespace Dominio.Interfaces;

using Dominio.Models;

public interface IDemanda
{
    void cadastrarDemanda(Demanda Demanda);
    void editarDemanda(Demanda service);
    void excluirDemanda(int id);
    abstract Demanda ObterPorId(int id);
    abstract Demanda ObterPorNumeroDemanda(int numeroDemanda);
    abstract Demanda ObterPorOrcamento(int idOrcamento);
    Demanda ObterPorResponsavel(int idResponsavel);
    Demanda ObterPorCliente(int idCliente);
    Demanda ObterPorStatus(string status);
    Demanda ObterPorDataDeFechamento(DateTime dataDeFechamento);
    Demanda ObterPorDataDeConclusao(DateTime dataDeConclusao);
    Demanda ObterPorDataDeEntrega(DateTime dataDeEntrega);
    Demanda ObterPorAtraso(string atraso);
    List<Demanda> ObterTodos();
}