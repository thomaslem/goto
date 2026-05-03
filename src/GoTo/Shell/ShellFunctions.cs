namespace GoTo.Shell;

public static class ShellFunctions
{
	public const string Marker = "# goto shell function";

	public static string Get(ShellType shell) => shell switch
	{
		ShellType.Bash or ShellType.Zsh =>
			$$"""

			  {{Marker}}
			  _goto_commands=
			  function gt() {
			      if [ -z "$_goto_commands" ]; then
			          _goto_commands="|$(goto --list-commands | tr '\n' '|')"
			      fi
			      if [[ "$1" == -* ]] || [[ "$_goto_commands" == *"|$1|"* ]]; then
			          goto "$@"
			      else
			          local dir
			          dir=$(goto get "$1")
			          if [ $? -eq 0 ]; then
			              cd "$dir"
			              if [ $# -gt 1 ]; then
			                  shift
			                  "$@"
			              fi
			          fi
			      fi
			  }
			  """,
		ShellType.PowerShell =>
			$$"""

			  {{Marker}}
			  $script:_gotoCommands = $null
			  function gt {
			      if ($null -eq $script:_gotoCommands) {
			          $script:_gotoCommands = goto --list-commands
			      }
			      if ($script:_gotoCommands -contains $args[0] -or $args[0] -like '-*') {
			          goto @args
			      } else {
			          $dir = goto get $args[0]
			          if ($LASTEXITCODE -eq 0) {
			              Set-Location $dir
			              if ($args.Count -gt 1) {
			                  $cmd = $args[1]
			                  $cmdArgs = if ($args.Count -gt 2) { $args[2..($args.Count - 1)] } else { @() }
			                  & $cmd @cmdArgs
			              }
			          }
			      }
			  }
			  """,
		_ => throw new ArgumentOutOfRangeException(nameof(shell))
	};
}
