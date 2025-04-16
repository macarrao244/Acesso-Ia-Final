<template>
    <div class="container mt-4">
        <h2 class="mb-3">Gerenciamento de Usuários</h2>

        <!-- Botão para criar novo -->
        <div class="mb-3">
            <router-link to="usuarios/cadastrar" class="btn btn-success">
                <i class="fa fa-plus"></i> Adicionar novo
            </router-link>
        </div>

        <div v-if="carregando" class="text-center">
            <div class="spinner-border text-primary" role="status">
                <span class="sr-only">Carregando...</span>
            </div>
        </div>

        <div v-if="erro" class="alert alert-danger text-center">
            <i class="fa fa-ban"></i> {{ erro }}
        </div>

        <div v-if="!carregando && usuarios.length === 0 && !erro" class="alert alert-warning text-center">
            <i class="fa fa-exclamation-circle"></i> Nenhum usuário cadastrado!
        </div>

        <!-- Tabela de listagem -->
        <div class="table-responsive">
            <table v-if="!carregando && usuarios.length > 0" class="table table-striped table-hover">
                <thead class="thead-dark">
                    <tr>
                        <th>Id</th>
                        <th>Nome</th>
                        <th>Email</th>
                        <th>Ações</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="usuario in usuarios" :key="usuario.id">
                        <td>{{ usuario.id }}</td>
                        <td>{{ usuario.nome }}</td>
                        <td>{{ usuario.email }}</td>
                        <td>
                            <button class="btn btn-info btn-sm" @click="abrirModal(usuario)">
                                <i class="fa fa-eye"></i> Detalhes
                            </button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>

        <!-- Modal de Detalhes -->
        <div v-if="usuarioSelecionado" class="modal fade show d-block" tabindex="-1">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title text-center">
                            Detalhes do Usuário do Id: 
                            {{ usuarioSelecionado.id }}
                        </h5>
                    </div>
                    <div class="modal-body text-left">
                        <div v-if="usuarioSelecionado.foto_perfil" 
                                  class="mb-3 text-center">
                            <img :src="'data:image/jpeg;base64,' 
                            + usuarioSelecionado.foto_perfil" 
                                 class="rounded-circle img-fluid"
                                 alt="Foto do usuário" width="100">
                        </div>
                        <p><strong>Nome:</strong> 
                            {{ usuarioSelecionado.nome }}
                        </p>
                        <p><strong>Email:</strong> 
                            {{ usuarioSelecionado.email }}
                        </p>
                        <p><strong>CPF:</strong> 
                            {{ usuarioSelecionado.cpf }}
                        </p>
                        <p><strong>Status:</strong> 
                            {{ usuarioSelecionado.status }}
                        </p>
                    </div>

                    <div class="modal-footer d-flex justify-content-between w-100">
                        <button class="btn btn-danger" 
                        @click="excluir(usuarioSelecionado.id)"> 
                            <i class="fa fa-trash"></i> Excluir
                        </button>
                        <div>
                            <router-link 
                            :to="`/home/usuarios/editar/${usuarioSelecionado.id}`" 
                                         class="btn btn-primary">
                                <i class="fa fa-edit"></i> Editar
                            </router-link>
                            <button type="button" 
                            class="btn btn-secondary ml-2" @click="fecharModal">Fechar</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Backdrop do Modal -->
        <div v-if="usuarioSelecionado" class="modal-backdrop fade show"></div>
    </div>
</template>

<script>
import axios from 'axios';
import Swal from 'sweetalert2';

export default {
    data() {
        return {
            usuarios: [],
            carregando: false,
            erro: null,
            usuarioSelecionado: null
        };
    },
    mounted() {
        this.carregarUsuarios();
    },
    methods: {
        async carregarUsuarios() {
            this.carregando = true;

            try {
                const response = await axios.get("https://localhost:7263/api/v1/usuarios/listar-todos");

                setTimeout(() => {
                    this.usuarios = response.data;
                    this.carregando = false;
                }, 2000);
            } catch (error) {
                this.erro = "Ocorreu um erro ao listar os usuários";
                console.log(error);
                this.carregando = false;
            }
        },
        async excluir(id) {
            const confirmacao = await Swal.fire({
                title: "Atenção",
                text: "Você deseja excluir este usuário?",
                icon: "warning",
                showCancelButton: true,
                confirmButtonText: "Sim, excluir",
                cancelButtonText: "Cancelar"
            });

            if (confirmacao.isConfirmed) {
                try {
                    await axios.delete(`https://localhost:7263/api/v1/usuarios/remover/${id}`);

                    Swal.fire("Excluído!", "O usuário foi removido com sucesso", "success");

                    this.fecharModal();
                    this.carregarUsuarios();
                } catch (error) {
                    Swal.fire("Erro!", "Ocorreu um erro ao excluir o usuário", "error");
                }
            }
        },
        abrirModal(usuario) {
            this.usuarioSelecionado = usuario;
        },
        fecharModal() {
            this.usuarioSelecionado = null;
        }
    }
}
</script>