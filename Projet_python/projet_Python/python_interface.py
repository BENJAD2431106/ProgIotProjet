import customtkinter as ctk
from tkinter import messagebox

# Configuration du thème
ctk.set_appearance_mode("dark")     # "light" ou "system"
ctk.set_default_color_theme("blue")  # thèmes : blue, green, dark-blue

# Fenêtre principale
app = ctk.CTk()
app.title("Connexion")
app.geometry("400x300")

# Titre
title = ctk.CTkLabel(app, text="Connexion", font=("Helvetica", 22, "bold"))
title.pack(pady=20)
text = ctk.CTkLabel(app, text="Connectez-vous afin d'accéder a Onyra Version Cute",font=("Helvetica", 12, "bold") )
text.pack(pady=5)

# Champ Username
username_entry = ctk.CTkEntry(app, placeholder_text="Nom d'utilisateur", width=250)
username_entry.pack(pady=10)

# Champ Password
password_entry = ctk.CTkEntry(app, placeholder_text="Mot de passe", show="•", width=250)
password_entry.pack(pady=10)

# Fonction de connexion
def login():
    username = username_entry.get()
    password = password_entry.get()

    # Exemple d'identifiants (à remplacer par un vrai système)
    if username == "admin" and password == "1234":
        messagebox.showinfo("Succès", "Connexion réussie !")
    else:
        messagebox.showerror("Erreur", "Identifiants incorrects.")

# Bouton de connexion
login_button = ctk.CTkButton(app, text="Se connecter", command=login, width=200)
login_button.pack(pady=20)

app.mainloop()
