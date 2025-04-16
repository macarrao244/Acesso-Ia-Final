<template>
    <div>
        <h2 class="text-center mb-4">Histórico de Acessos</h2>
   
        <div v-if="carregando" class="text-center">
            <i class="fa fa-spinner fa-spin"></i> Carregando....
        </div>

        <div v-if="erro" class="alert alert-danger">
            <i class="fa fa-ban"></i>
            {{ erro }}
        </div>

        <div v-if="!carregando && acessos.length === 0 && !erro" class="alert alert-warning text-center">
            <i class="fa fa-exclamation-circle"></i> Nenhum acesso cadastrado!
        </div>

        <!-- Tabela de listagem -->
        <table v-if="!carregando && acessos.length > 0" class="table table-striped">
            <thead class="thead-dark">
                <tr>
                    <th>Id</th>
                    <th>Nome do Usuário</th>
                    <th>Data/Hora de Acesso</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="acesso in acessos" :key="acesso.id">
                    <td>{{ acesso.id }}</td>
                    <td>{{ acesso.nomeUsuario }}</td>
                    <td>{{ acesso.dataHoraAcesso }}</td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
import axios from 'axios';
export default {
    data() {
        return {
            acessos: [],
            carregando: false,
            erro: null
        };
    },
    mounted() {
        this.carregarAcessos();
    },
    methods: {
        async carregarAcessos() {
            this.carregando = true;
            try {
                const response = await axios.get("https://localhost:7263/api/v1/acessos/listar-todos");
                console.log(response.data); 
                this.acessos = response.data;
            } catch (error) {
                this.erro = "Ocorreu um erro ao listar os acessos";
                console.error(error);
            } finally {
                this.carregando = false;
            }
        }
    }
}
</script>
