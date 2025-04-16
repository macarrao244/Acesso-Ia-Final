<template>
  <div class="container mt-5">
    <div class="row justify-content-center">
      <div class="col-md-6">
        <div class="card shadow p-4">
          <h3 class="text-center mb-4">Recuperar Senha</h3>

          <form @submit.prevent="enviarEmail">
            <div class="form-group">
              <label for="email">Informe seu email</label>
              <input
                v-model="email"
                type="email"
                class="form-control"
                placeholder="Digite seu email"
                required
              />
            </div>

            <div class="form-group mt-3">
              <label for="senha">Nova Senha</label>
              <input
                v-model="senha"
                type="password"
                class="form-control"
                placeholder="Digite sua nova senha"
                required
              />
            </div>

            <div class="form-group mt-3">
              <label for="confirmarSenha">Confirmar Senha</label>
              <input
                v-model="confirmarSenha"
                type="password"
                class="form-control"
                placeholder="Confirme sua nova senha"
                required
              />
            </div>

            <div class="text-center mt-4">
              <button class="btn btn-primary" type="submit">
                <i class="fa fa-envelope"></i> Recuperar Senha
              </button>
              <router-link to="/login" class="btn btn-link d-block mt-2">
                <i class="fa fa-arrow-left"></i> Voltar para o Login
              </router-link>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import axios from "axios";
import Swal from "sweetalert2";

export default {
  data() {
    return {
      email: "",
      senha: "",
      confirmarSenha: ""
    };
  },
  methods: {
    async enviarEmail() {
      if (!this.email || !this.senha || !this.confirmarSenha) {
        Swal.fire({
          icon: "warning",
          title: "Campos obrigatórios",
          text: "Por favor, preencha todos os campos.",
        });
        return;
      }

      if (this.senha !== this.confirmarSenha) {
        Swal.fire({
          icon: "error",
          title: "Senhas diferentes",
          text: "As senhas não coincidem.",
        });
        return;
      }

      try {
        const response = await axios.get(
          `https://localhost:7263/api/v1/login/buscar-email?email=${this.email}`
        );
        const usuario = response.data;

        await axios.put(
          `https://localhost:7263/api/v1/login/resetar-senha/${usuario.id}`,
          { novaSenha: this.senha }
        );

        Swal.fire({
          icon: "success",
          title: "Senha redefinida!",
          text: "Sua senha foi atualizada com sucesso.",
        });

        this.email = "";
        this.senha = "";
        this.confirmarSenha = "";
        this.$router.push("/login");

      } catch (error) {
        const msg = error.response?.data || "Erro ao buscar o usuario";
        Swal.fire({
          icon: "error",
          title: "Email não encontrado",
          text: typeof msg === "string" ? msg : msg.title || msg.errors?.Usuario?.[0] || "O email informado não está cadastrado.",
        });
      }
    },
  },
};
</script>
