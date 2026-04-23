namespace GoTo.Shell;

public static class ShellFunctions
{
	public const string Marker = "# goto shell function";

	public static string Get(ShellType shell) => shell switch
	{
		ShellType.Bash or ShellType.Zsh =>
			$$"""

			  {{Marker}}
			  function gt() {
			      case "$1" in
			          add|get|init|-*)
			              goto "$@"
			              ;;
			          *)
			              local dir
			              dir=$(goto get "$1")
			              if [ $? -eq 0 ]; then
			                  cd "$dir"
			                  if [ $# -gt 1 ]; then
			                      shift
			                      "$@"
			                  fi
			              fi
			              ;;
			      esac
			  }
			  """,
		ShellType.PowerShell =>
			$$"""

			  {{Marker}}
			  function gt {
			      $subcommands = @('add', 'get', 'init')
			      if ($subcommands -contains $args[0] -or ($args[0] -like '-*')) {
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
