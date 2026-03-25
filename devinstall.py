import os
import shutil

# Change directory to the user's home directory
os.chdir(os.path.expanduser("~"))

# Print the current working directory
print("Current working directory:", os.getcwd())

# Define paths
src_base = os.path.join("Documents", "workspace", "microting", "eform-angular-basecustomer-plugin")
dst_base = os.path.join("Documents", "workspace", "microting", "eform-angular-frontend")

# List of files and directories to remove
files_to_remove = [
    os.path.join("eform-client", "src", "app", "plugins", "modules", "customers-pn"),
    os.path.join("eform-client", "e2e", "Tests", "customer-settings"),
    os.path.join("eform-client", "e2e", "Tests", "customer-general"),
    os.path.join("eform-client", "e2e", "Assets"),
    os.path.join("eform-client", "e2e", "Page objects", "Customers"),
    os.path.join("eform-client", "wdio-plugin-step2.conf.js"),
]

# Remove files and directories
for rel_path in files_to_remove:
    full_path = os.path.join(dst_base, rel_path)
    if os.path.exists(full_path):
        if os.path.isdir(full_path):
            shutil.rmtree(full_path)
        else:
            os.remove(full_path)

# List of files and directories to copy
files_to_copy = [
    (os.path.join("eform-client", "src", "app", "plugins", "modules", "customers-pn"),
     os.path.join("eform-client", "src", "app", "plugins", "modules", "customers-pn")),
    (os.path.join("eform-client", "e2e", "Tests", "customer-settings"),
     os.path.join("eform-client", "e2e", "Tests", "customer-settings")),
    (os.path.join("eform-client", "e2e", "Tests", "customer-general"),
     os.path.join("eform-client", "e2e", "Tests", "customer-general")),
    (os.path.join("eform-client", "e2e", "Assets"),
     os.path.join("eform-client", "e2e", "Assets")),
    (os.path.join("eform-client", "e2e", "Page objects", "Customers"),
     os.path.join("eform-client", "e2e", "Page objects", "Customers")),
    (os.path.join("eform-client", "wdio-headless-plugin-step2.conf.js"),
     os.path.join("eform-client", "wdio-plugin-step2.conf.js")),
]

# Copy files and directories
for src_rel_path, dst_rel_path in files_to_copy:
    src_path = os.path.join(src_base, src_rel_path)
    dst_path = os.path.join(dst_base, dst_rel_path)

    if os.path.isdir(src_path):
        shutil.copytree(src_path, dst_path)
    else:
        shutil.copy2(src_path, dst_path)

# Remove and copy backend plugin
backend_dst = os.path.join(dst_base, "eFormAPI", "Plugins", "Customers.Pn")
backend_src = os.path.join(src_base, "eFormAPI", "Plugins", "Customers.Pn")

if os.path.exists(backend_dst):
    shutil.rmtree(backend_dst)

shutil.copytree(backend_src, backend_dst)
