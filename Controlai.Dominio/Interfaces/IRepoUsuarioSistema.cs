namespace Dominio.Interfaces;

using Dominio.Models;
public interface IRepoUsuarioSistema
{
    Task<UsuarioSistema> CadastrarUsuario(UsuarioSistema usuario);
    Task<UsuarioSistema> EditarUsuario(UsuarioSistema usuario);
    Task ExcluirUsuario(int id);
    Task<UsuarioSistema> ObterPorId(int id);
    Task<List<UsuarioSistema>> ObterPorNome(string nome);
    Task<List<UsuarioSistema>> ObterPorPerfil(bool isAdministrador);
    Task<UsuarioSistema> ObterPorUsuario(string usuario);
    Task<List<UsuarioSistema>> ObterPorDataDeCadastro(DateTime dataDeCadastro);
    Task<UsuarioSistema> ObterPorEmail(string email);
    Task<List<UsuarioSistema>> ObterTodos();
}
