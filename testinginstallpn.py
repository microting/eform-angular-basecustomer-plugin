import re
import os

file_path = os.path.join("src", "app", "plugins", "plugins.routing.ts")
with open(file_path, "r") as file:
    content = file.read()

replacements = [
    (r"// INSERT ROUTES HERE", "  {\n// INSERT ROUTES HERE"),
    (r"// INSERT ROUTES HERE", "    path: 'customers-pn',\n// INSERT ROUTES HERE"),
    (r"// INSERT ROUTES HERE", "    loadChildren: () => import('./modules/customers-pn/customers-pn.module')\n// INSERT ROUTES HERE"),
    (r"// INSERT ROUTES HERE", "      .then(m => m.CustomersPnModule)\n// INSERT ROUTES HERE"),
    (r"// INSERT ROUTES HERE", "  },\n// INSERT ROUTES HERE"),
]

for pattern, replacement in replacements:
    content = re.sub(pattern, replacement, content, count=1)

with open(file_path, "w") as file:
    file.write(content)
