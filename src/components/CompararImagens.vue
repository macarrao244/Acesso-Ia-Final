<template>
    <div class="container mt-4">
      <h2 class="text-center mb-4">Comparar Imagens</h2>
  
      <div class="row">
        <!-- Dropzone 1 -->
        <div class="col-md-6">
          <label>Imagem 1</label>
          <div ref="dropzone1" class="dropzone mb-3"></div>
          <div v-if="preview1" class="text-center">
            <img :src="preview1" class="img-preview img-fluid" alt="Preview 1" />
          </div>
        </div>
  
        <!-- Dropzone 2 -->
        <div class="col-md-6">
          <label>Imagem 2</label>
          <div ref="dropzone2" class="dropzone mb-3"></div>
          <div v-if="preview2" class="text-center">
            <img :src="preview2" class="img-preview img-fluid" alt="Preview 2" />
          </div>
        </div>
      </div>
  
      <div class="text-center mt-4">
        <button class="btn btn-primary" @click="compararImagens">
          <i class="fa fa-search"></i> Comparar
        </button>
      </div>
    </div>
  </template>
  
  <script>
  import Dropzone from "dropzone";
  import "dropzone/dist/dropzone.css";
  import Swal from "sweetalert2";
  
  export default {
    data() {
      return {
        base64_1: "",
        base64_2: "",
        preview1: null,
        preview2: null
      };
    },
    mounted() {
      this.iniciarDropzones();
    },
    methods: {
      iniciarDropzones() {
        const self = this;
  
        new Dropzone(this.$refs.dropzone1, {
          url: "/",
          autoProcessQueue: false,
          acceptedFiles: "image/*",
          maxFiles: 1,
          addRemoveLinks: true,
          dictDefaultMessage: "Arraste ou clique para enviar a Imagem 1",
          init: function () {
            this.on("addedfile", function (file) {
              self.converterImagem(file, "base64_1", "preview1");
            });
            this.on("removedfile", function () {
              self.base64_1 = "";
              self.preview1 = null;
            });
          }
        });
  
        new Dropzone(this.$refs.dropzone2, {
          url: "/",
          autoProcessQueue: false,
          acceptedFiles: "image/*",
          maxFiles: 1,
          addRemoveLinks: true,
          dictDefaultMessage: "Arraste ou clique para enviar a Imagem 2",
          init: function () {
            this.on("addedfile", function (file) {
              self.converterImagem(file, "base64_2", "preview2");
            });
            this.on("removedfile", function () {
              self.base64_2 = "";
              self.preview2 = null;
            });
          }
        });
      },
  
      converterImagem(file, base64Prop, previewProp) {
        const reader = new FileReader();
        reader.onload = (e) => {
          this[base64Prop] = e.target.result.split(",")[1];
          this[previewProp] = e.target.result;
        };
        reader.readAsDataURL(file);
      },
  
      compararImagens() {
        if (!this.base64_1 || !this.base64_2) {
          Swal.fire({
            icon: 'warning',
            title: 'Atenção!',
            text: 'Você precisa enviar as duas imagens antes de comparar.',
          });
          return;
        }
  
        const saoIguais = this.base64_1 === this.base64_2;
  
        Swal.fire({
          icon: saoIguais ? 'success' : 'error',
          title: saoIguais ? 'Imagens Iguais' : 'Imagens Diferentes',
          text: saoIguais 
            ? 'As duas imagens enviadas são idênticas!'
            : 'As imagens são diferentes. Verifique novamente.',
        });
      }
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
  .dropzone {
    border: 2px dashed #007bff;
    border-radius: 10px;
    padding: 20px;
    text-align: center; 
    background-color: #f8f9fa; 
  }
  .img-preview {
  max-width: 100%;
  max-height: 300px;
  border-radius: 10px;
  border: 3px solid #ddd;
  }
  </style>
  