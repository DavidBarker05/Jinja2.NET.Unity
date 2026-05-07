using System.Collections.Generic;

namespace Jinja2.NET.Interfaces
{
	public interface ILexer
	{
		List<Token> Tokenize();
	}
}