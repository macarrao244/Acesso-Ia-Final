<template>
  <div class="container mt-4">
    <div class="row justify-content-center">
      <div class="col-md-6">
        <h2 class="text-center mb-4">Registrar Acesso</h2>
  
        <!-- Formulário de Acesso -->
        <form @submit.prevent="registrarAcesso">
          <div class="form-group mb-4">
            <label for="usuario">Usuário</label>
            <select v-model="acesso.idUsuario" class="form-control" required>
              <option value="" disabled selected>Selecione um usuário</option>
              <option v-for="usuario in usuarios" :value="usuario.id" :key="usuario.id">
                {{ usuario.nome }}
              </option>
            </select>
          </div>
  
          <div class="form-group mb-4">
            <label for="dataHora">Data e Hora do Acesso</label>
            <input type="datetime-local" v-model="acesso.dataHoraAcesso" class="form-control" required>
          </div>
      
          <div class="text-center">
            <button type="submit" class="btn btn-primary mr-2">
              <i class="fa fa-save"></i> Salvar
            </button>
            <button type="button" class="btn btn-secondary" @click="voltar">
              <i class="fa fa-arrow-left"></i> Voltar
            </button>
          </div>
        </form>
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
      acesso: {
        idUsuario: "",
        dataHoraAcesso: this.formatarDataHoraAtual()
      },
      usuarios: []
    };
  },
  async created() {
    await this.carregarUsuarios();
  },
  methods: {
    formatarDataHoraAtual() {
      const now = new Date();
      return now.toISOString().slice(0, 16);
    },
    async carregarUsuarios() {
      try {
        const { data } = await axios.get("https://localhost:7263/api/v1/usuarios/listar-todos");
        this.usuarios = data;
      } catch (error) {
        console.error("Erro ao carregar usuários:", error);
        Swal.fire({
          title: "Erro!",
          text: "Não foi possível carregar a lista de usuários.",
          icon: "error",
          confirmButtonText: "OK"
        });
      }
    },
    async registrarAcesso() {
      try {
        await axios.post(
          "https://localhost:7263/api/v1/acessos/registrar", 
          this.acesso
        );
        
        Swal.fire({
          title: "Sucesso!",
          text: "Acesso registrado com sucesso. Redirecionando...",
          icon: "success",
          timer: 1500, // Fecha automaticamente após 1.5 segundos
          showConfirmButton: false
        });

        // Redireciona após o timer do SweetAlert
        setTimeout(() => {
          this.$router.push("/home/acesso/listar");
        }, 1500);
        
      } catch (error) {
        let errorMessage = "Falha ao registrar acesso.";
        
        if (error.response && error.response.status === 500) {
          errorMessage = "O usuário selecionado não existe no sistema.";
        }

        Swal.fire({
          title: "Erro!",
          text: errorMessage,
          icon: "error",
          confirmButtonText: "OK"
        });
        console.error(error);
      }
    },
    voltar() {
      this.$router.go(-1);
    }
  }
};
</script>

<style scoped>
.container {
  max-width: 600px;
}

h2 {
  font-weight: bold;
  color: #333;
}

.form-group label {
  font-weight: 500;
  color: #495057;
}

.btn {
  min-width: 100px;
}

.btn-primary {
  background-color: #007bff;
  border-color: #007bff;
}

.btn-secondary {
  background-color: #6c757d;
  border-color: #6c757d;
}
</style>