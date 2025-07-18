namespace Applicacao.Servicos;

using BCrypt.Net;
using static BCrypt.Net.BCrypt;
using System.Text.RegularExpressions;
using System.Security.Cryptography;


public class SvcCryptSenha
{
    public static string GerarHash(string senha)
    {
        if (string.IsNullOrWhiteSpace(senha)) throw new Exception("Senha precisa ter conteúdo");

        return HashPassword(senha);
    }

    public static bool VerificarHash(string Senha, string SenhaHash)
    {
        if (string.IsNullOrWhiteSpace(Senha)) throw new Exception("Senha precisa ter conteúdo");
        if (string.IsNullOrWhiteSpace(SenhaHash)) throw new Exception("SenhaHash precisa ter conteúdo");

        if (!RegexVerify(SenhaHash))
        {
            var contagem = SenhaHash.Count();

            SenhaHash = GerarHash(SenhaAleatoria(contagem));
        }

        return Verify(Senha, SenhaHash);
    }

    public static bool RegexVerify(string Senha)
    {
        if (string.IsNullOrWhiteSpace(Senha)) throw new Exception("Senha precisa ter conteúdo");

        var regex = new Regex(@"^\$2[aby]?\$\d{2}\$[./A-Za-z0-9]{53}$");
        var reg = regex.IsMatch(Senha);

        return reg;

    }


    public static string SenhaAleatoria (int tamanho = 12)
    {
        const string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%&*";

        var bytes = new byte[tamanho];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(bytes);
        }

        var senha = new char[tamanho];

        for (int i = 0; i < tamanho; i++)
        {
            senha[i] = caracteres[bytes[i] % caracteres.Length];
        }

        return new string(senha);
    }

}
