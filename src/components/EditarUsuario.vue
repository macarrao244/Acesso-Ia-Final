<template>
  <div class="container mt-4">
    <div class="row justify-content-center">
      <div class="col-md-8">
        <h2 class="text-center mb-4">Editar Usuário</h2>
  
        <!-- Formulário -->
        <form @submit.prevent="atualizarUsuario">
          <div class="form-group mb-4">
            <label for="nome">Nome</label>
            <input type="text" v-model="usuario.nome" class="form-control" required placeholder="Digite o nome">
          </div>
  
          <div class="form-group mb-4">
            <label for="email">Email</label>
            <input type="email" v-model="usuario.email" class="form-control" required placeholder="Digite o email">
          </div>
  
          <div class="form-group mb-4">
            <label for="cpf">CPF</label>
            <input type="text" v-model="usuario.cpf" class="form-control" required placeholder="Digite o CPF">
          </div>
  
          <div class="form-group mb-4">
            <label for="status">Status</label>
            <select v-model="usuario.status" class="form-control" required>
              <option value="ativo">Ativo</option>
              <option value="inativo">Inativo</option>
            </select>
          </div>
      
          <div class="text-center">
            <button type="submit" class="btn btn-success">
              <i class="fa fa-save"></i> Salvar
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
    usuario: {
      id:"",
      nome: "",
      email: "",
      cpf: "",
      status: "ativo",
      foto: ""
    },
 
  };
},

mounted(){
  this.buscarDadosUsuario();
},
methods: {
  async buscarDadosUsuario()
  {
    const usuarioId = this.$route.params.id;
    try{
      const response = await axios.get(`https://localhost:7263/api/v1/usuarios/buscar/${usuarioId}`);
      this.usuario = response.data;
    } catch (error) {
      Swal.fire({
        title: "Erro!",
        text: "Usuario nao encontrado!",
        icon: "error",
        confirmButtonText: "OK"
      });
      console.error(error);
      this.$router.push("/home/usuarios"); 
    }
  },
  async atualizarUsuario() {
    try {
      const response = await axios.put(`https://localhost:7263/api/v1/usuarios/atualizar/${this.usuario.id}`, this.usuario);
      console.log(response);
      Swal.fire({
        title: "Sucesso!",
        text: "Usuário atualizado com sucesso.",
        icon: "success",
        confirmButtonText: "OK"
      });

      this.$router.push("/home/usuarios"); 
    } catch (error) {
      Swal.fire({
        title: "Erro!",
        text: "Falha ao cadastrar usuário.",
        icon: "error",
        confirmButtonText: "OK"
      });
      console.error(error);
    }
  },
}
};
</script>

<style scoped>
 .container {
max-width: 800px;
}

h2 {
font-weight: bold;
}
</style>
