<template>  
  <div>
    <!--Navbar-->
    <nav class="navbar navbar-expand-lg navbar-dark bg-primary">
      <div class="container">
        <a class="navbar-brand" href="#">
          <img src="@/assets/logo.png" class="logo"> Sistema IA
        </a>

        <!-- Menu Usuário -->
         <div class="collapse navbar-collapse">
          <ul class="navbar-nav">
            <li class="nav-item dropdown">
              <router-link class="nav-link dropdown-toggle"
              to="/home/usuarios">
              Usuários
              </router-link>
           
            </li>
             
            <router-link class="nav-link "
              to="/home/acesso/registrar">
                Registrar Acessos
              </router-link>
              <router-link class="nav-link "
              to="/home/acesso/listar">
                Listar Acessos
              </router-link>
              <router-link class="nav-link "
              to="/home/usuarios/comparar">
                Comparar Imagens
              </router-link>
              
          </ul>
         
         </div>
      </div>
      
      
      <!-- Botão Sair -->
      <ul class="navbar-nav ml-auto">
        <li class="nav-item">
          <a class="nav-link text-white" href="#" @click="deslogar">
            <i class="fa fa-sign-out" aria-hidden="true"></i> Sair
          </a>
        </li>
      </ul>
    </nav>

      <div v-if="exibirBemvindo" class="container mt-4 text-center">
      <h2>Bem vindo, {{ nomeUsuarioLogado }}</h2>
    </div>

    <!-- Troca de forma dinamica o conteudo central com base na url -->
    <div class="container">
      <router-view></router-view>
    </div>

  </div>
</template>

<script>   
export default {
  data(){
    return {
      nomeUsuarioLogado: ''
    };
  },
  computed: {
    exibirBemvindo()
    {
      return this.$route.path === '/home';
    }
  },
  mounted(){
    const usuarioLogado = localStorage.getItem('dados-usuario-logado');
    if(usuarioLogado){
      this.nomeUsuarioLogado = JSON.parse(usuarioLogado).nome;
    }
  },
  methods: {
      deslogar()
      {
          localStorage.removeItem('dados-usuario-logado');
          this.$router.push('/login');
      }
  }
}
</script>

<style>
.logo{
  width: 30px;
  margin-right: 10px;
}
</style>