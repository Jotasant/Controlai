using static BCrypt.Net.BCrypt;

namespace Applicacao.Servicos
{
    public class SvcCryptSenha
    {
        public static string? GerarHash(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha)) throw new Exception("Senha precisa ter conteúdo");

            return HashPassword(senha);
        }

        public static bool VerificarHash(string Senha, string SenhaHash)
        {
            if (string.IsNullOrWhiteSpace(Senha)) throw new Exception("Senha precisa ter conteúdo");
            if (string.IsNullOrWhiteSpace(SenhaHash)) throw new Exception("SenhaHash precisa ter conteúdo");

            return Verify(Senha, SenhaHash);
        }

    }
}