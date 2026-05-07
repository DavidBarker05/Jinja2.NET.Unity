using Jinja2.NET.Nodes;

namespace Jinja2.NET.Interfaces
{
	public interface IStatementParser
	{
		public (ASTNode Node, ETokenType ConsumedStartMarkerType) Parse(TokenIterator tokens);
		public (ASTNode Node, ETokenType ConsumedStartMarkerType) Parse(TokenIterator tokens, params string[] stopKeywords);
	}
}