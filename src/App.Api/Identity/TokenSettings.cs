namespace App.Api.Identity
{
    public class TokenSettings
    {
        /// <summary>
        /// Secret utilizado na criptografia do token (deve ser algo complexo de ser quebrado)
        /// </summary>
        public string Secret { get; set; }
        /// <summary>
        /// Tempo para expirar o token após a emissão
        /// </summary>
        public int HoursToExpire { get; set; }
        /// <summary>
        /// Emissor do token
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// Quais urls aceitam o token gerado
        /// </summary>
        public string Audience { get; set; }
    }
}
