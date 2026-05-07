using System;
using System.Linq;
using Jinja2.NET.Interfaces;

namespace Jinja2.NET.Nodes.Renderers.BlockNodeSupport
{
	public class SetBlockRenderer : INodeRenderer
	{
		public object Render(ASTNode node, IRenderer renderer)
		{
			if (node is not BlockNode setNode ||
				!setNode.Name.Equals(TemplateConstants.BlockNames.Set, StringComparison.OrdinalIgnoreCase))
			{
				throw new ArgumentException($"Expected Set BlockNode, got {node.GetType().Name}");
			}

			var identifiers = setNode.Arguments.Take(setNode.Arguments.Count - 1).OfType<IdentifierNode>().ToList();
			var valueNode = setNode.Arguments.Last();
			var value = renderer.Visit(valueNode);

			foreach (var identifier in identifiers)
			{
				var currentScope = renderer.ScopeManager.CurrentScope();

				if (currentScope.ContainsKey("loop"))
				{
					// In a loop: set only in current scope
					currentScope[identifier.Name] = value;
				}
				else if (IsAtGlobalScope(renderer.ScopeManager))
				{
					// At global level: set in both
					currentScope[identifier.Name] = value;
					renderer.Context.Set(identifier.Name, value);
				}
				else
				{
					// In an if or other block: set only in current scope
					currentScope[identifier.Name] = value;
				}
			}


			return null;
		}

		private static bool IsAtGlobalScope(IScopeManager scopeManager)
		{
			// One scope means global.
			// Prefer ScopeDepth when an implementation exposes it.
			var scopeDepthProp = scopeManager.GetType().GetProperty("ScopeDepth");
			if (scopeDepthProp?.PropertyType == typeof(int))
			{
				var value = scopeDepthProp.GetValue(scopeManager);
				if (value is int depth)
				{
					return depth == 1;
				}
			}
			// Fallback for implementations without ScopeDepth
			return ReferenceEquals(scopeManager.CurrentScope(), scopeManager.ParentScope());
		}
	}
}
