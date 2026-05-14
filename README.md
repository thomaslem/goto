# goto

A CLI tool for managing directory aliases, letting you quickly navigate to frequently-used paths.

## Installation

```bash
dotnet pack
dotnet tool install --global --source ./artifacts/package/release/ goto
```

Then set up the shell function (required for `cd` integration):

```bash
goto init
```

Restart your shell or source your profile.

## Usage

Use `gt <alias>` to jump to a directory. Optionally pass a command to run there.

```bash
gt project            # cd to the 'project' alias
gt project code .     # cd to 'project', then run 'code .'
```

### Commands

```bash
gt add <alias> [path]   # Add or update an alias (defaults to current directory)
gt remove <alias>       # Remove an alias
gt list                 # List all aliases
gt get <alias>          # Print the path for an alias
```

## Storage

Aliases are stored in `~/.goto/aliases.json`.
