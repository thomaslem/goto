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
			          add|get|init)
			              goto "$@"
			              ;;
			          *)
			              local dir
			              dir=$(goto get "$@")
			              if [ $? -eq 0 ]; then
			                  cd "$dir"
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
			      if ($subcommands -contains $args[0]) {
			          goto @args
			      } else {
			          $dir = goto get @args
			          if ($LASTEXITCODE -eq 0) {
			              Set-Location $dir
			          }
			      }
			  }
			  """,
		_ => throw new ArgumentOutOfRangeException(nameof(shell))
	};
}
