from pathlib import Path


RUNTIME_DIR = Path("Runtime")


def replace_spaces_in_file(cs_file: Path) -> bool:
    original = cs_file.read_text(encoding="utf-8")
    updated = original.replace("    ", "\t")

    if updated == original:
        return False

    cs_file.write_text(updated, encoding="utf-8")
    return True


def main() -> None:
    if not RUNTIME_DIR.exists():
        print(f"Directory not found: {RUNTIME_DIR}")
        return

    updated_count = 0
    for cs_file in RUNTIME_DIR.rglob("*.cs"):
        if replace_spaces_in_file(cs_file):
            updated_count += 1
            print(f"Updated: {cs_file}")

    print(f"Done. Updated {updated_count} file(s).")


if __name__ == "__main__":
    main()
