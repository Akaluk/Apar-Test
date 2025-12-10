<template>
  <div class="overlay">
    <div class="modal">
      <h3>ยืนยันการอนุมัติ</h3>
      <textarea v-model="reason" rows="6" style="width:100%"></textarea>
      <div style="text-align:right; margin-top:8px;">
        <button @click="$emit('close')" style="margin-right:8px;">ยกเลิก</button>
        <button @click="confirm" style="background:#0b74de;color:white;padding:6px 12px;border:none;border-radius:6px; cursor:pointer;">อนุมัติ</button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: ['ids'],
  data() { return { reason: '' } },
  methods: {
    confirm() {
      if (!this.reason) { alert('กรุณาใส่เหตุผล'); return; }
      this.$emit('confirmed', { ids: this.ids, reason: this.reason })
      this.$emit('close')
      this.reason = ''
    }
  }
}
</script>

<style>
.overlay { position: fixed; inset:0; background: rgba(0,0,0,0.4); display:flex; align-items:center; justify-content:center; }
.modal { background:white; padding:16px; width:600px; border-radius:6px; box-shadow:0 6px 18px rgba(0,0,0,0.25); }
</style>