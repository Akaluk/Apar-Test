<template>
  <div v-if="show" class="modal-overlay">
    <div class="modal-content">
      <h3>เพิ่มข้อมูลใหม่</h3>
      <label>รายการ:</label>
      <textarea v-model="title" rows="6" style="width:100%"></textarea>
      <div style="margin-top:8px; text-align:right;">
        <button @click="confirm" style="background:#006400;color:white;padding:6px 12px;border:none;border-radius:6px; cursor:pointer; margin-right:8px;">บันทึก</button>
        <button @click="$emit('close') ">ยกเลิก</button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    show: { type: Boolean, default: false }
  },
  data() {
    return { title: '' }
  },
  methods: {
    confirm() {
      if (!this.title) return
      this.$emit('confirmed', { title: this.title }) 
      this.title = '' 
    }
  }
}
</script>

<style scoped>
.modal-overlay {
  position: fixed;
  top:0; left:0; right:0; bottom:0;
  background-color: rgba(0,0,0,0.5);
  display:flex;
  align-items:center;
  justify-content:center;
  z-index:1000;
}

.modal-content {
  background:white;
  padding:16px;
  border-radius:8px;
  width:600px;
}
</style>
