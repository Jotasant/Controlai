namespace Dominio.Interfaces;

using Dominio.Models;

public interface ICliente
{
    void CadastrarCliente(Cliente cliente);
    void EditarCliente(Cliente cliente);
    void ExcluirCliente(int id);
    Cliente ObterPorId(int id);
    Cliente ObterPorNome(string nome);
    Cliente ObterPorDataDeCadastro(DateTime dataDeCadastro);
    Cliente ObterPorDataDeNascimento(DateTime dataDeNascimento);
    Cliente ObterPorCondominio(string condominio);
    List<Cliente> ObterTodos();
}
