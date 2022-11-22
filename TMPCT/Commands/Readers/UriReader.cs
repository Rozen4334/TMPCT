namespace TMPCT.Commands.Readers
{
    public class UriReader : TypeReader<Uri>
    {
        public override Task<TypeReaderResult> ReadAsync(IContext context, Parameter info, object value, IServiceProvider provider)
        {
            if (Uri.TryCreate(value.ToString(), UriKind.Absolute, out var result))
                return Task.FromResult(TypeReaderResult.FromSuccess(result));

            return Task.FromResult(TypeReaderResult.FromError("Failed to interpret web url from input string."));
        }
    }
}
