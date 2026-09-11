#!/usr/bin/env bash
# Crea tutte le issue di Library Manager API su GitHub a partire da issues.csv
#
# Prerequisiti:
#   1. GitHub CLI installato (https://cli.github.com/)
#   2. Autenticato con: gh auth login
#   3. La repo deve già esistere su GitHub
#
# Uso:
#   ./create_issues.sh tuo-utente/library-manager-api
#
# (assicurati che le label usate nel CSV - setup, architecture, feature,
#  cleanup, testing, docs - esistano già nella repo, oppure crea prima
#  con: gh label create "nome-label" --repo tuo-utente/library-manager-api)

set -euo pipefail

REPO="${1:?Uso: ./create_issues.sh utente/nome-repo}"
CSV_FILE="$(dirname "$0")/issues.csv"

if ! command -v gh &> /dev/null; then
  echo "Errore: GitHub CLI (gh) non trovato. Installalo da https://cli.github.com/"
  exit 1
fi

echo "Creazione issue su $REPO da $CSV_FILE ..."

python3 - "$CSV_FILE" "$REPO" << 'PYEOF'
import csv
import subprocess
import sys

csv_file, repo = sys.argv[1], sys.argv[2]

with open(csv_file, newline="", encoding="utf-8") as f:
    reader = csv.DictReader(f)
    for row in reader:
        title = row["Title"]
        body = row["Body"]
        labels = row["Labels"]
        print(f"-> Creo issue: {title}")
        subprocess.run(
            [
                "gh", "issue", "create",
                "--repo", repo,
                "--title", title,
                "--body", body,
                "--label", labels,
            ],
            check=True,
        )

print("Fatto! Tutte le issue sono state create.")
PYEOF
