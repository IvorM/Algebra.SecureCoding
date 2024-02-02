namespace Algebra.SecureCoding.Web.Models.Shared
{
    public class Result<T>
    {
        public bool IsOk { get; set; }
        public string? ErrorMessage { get; set; }
        public T? Value { get; set; }
    }
}
