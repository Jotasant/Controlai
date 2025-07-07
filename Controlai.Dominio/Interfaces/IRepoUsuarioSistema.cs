namespace Dominio.Interfaces;

using Dominio.Models;
public interface IRepoUsuarioSistema
{
    void CadastrarUsuario(UsuarioSistema usuario);
    void EditarUsuario(UsuarioSistema usuario);
    void ExcluirUsuario(int id);
    UsuarioSistema ObterPorId(int id);
    List<UsuarioSistema> ObterPorNome(string nome);
    List<UsuarioSistema> ObterPorPerfil(bool isAdministrador);
    UsuarioSistema ObterPorUsuario(string usuario);
    List<UsuarioSistema> ObterPorDataDeCadastro(DateTime dataDeCadastro);
    UsuarioSistema ObterPorEmail(string email);
    List<UsuarioSistema> ObterTodos();
}
