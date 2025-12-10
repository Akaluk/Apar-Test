<template>
  <div style="padding:16px; max-width:1000px; margin:auto;">
    <h2 style="background:#0a0; color:white; padding:8px; text-align:center;">IT 03</h2>
    <div style="margin:8px 0;">
      <button @click="showApprove = true"
              style="background:#2e86de;color:white;padding:8px 16px;border:none;border-radius:6px; cursor:pointer; margin-right:8px;">
        อนุมัติ
      </button>
      <button @click="showReject = true"
              style="background:#c0392b;color:white;padding:8px 16px;border:none;border-radius:6px; cursor:pointer;">
        ไม่อนุมัติ
      </button>
    </div>
    <table border="1" cellpadding="8" cellspacing="0" width="100%">
      <thead style="background:#2f78b6;color:white;">
        <tr>
          <th style="width:60px"></th>
          <th>รายการ</th>
          <th>เหตุผล</th>
          <th>สถานะเอกสาร</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in items" :key="item.id">
          <td style="text-align:center;">
            <input type="checkbox"
                  :disabled="item.status !== 'pending'"
                  :value="item.id"
                  v-model="selected" />
          </td>
          <td>{{ item.title }}</td>
          <td>{{ item.reason }}</td>
          <td>{{ statusLabel(item.status) }}</td>
        </tr>
      </tbody>
    </table>
    <div style="text-align:right; margin-top:8px;">
      <button @click="showAdd = true"
              style="background:#006400;color:white;padding:8px 16px;border:none;border-radius:6px; cursor:pointer;">
        เพิ่มข้อมูล
      </button>
    </div>

    <ApproveModal v-if="showApprove" :ids="selected" @close="handleClose" @confirmed="handleApprove" />
    <RejectModal v-if="showReject" :ids="selected" @close="handleClose" @confirmed="handleReject" />
    <AddModal :show="showAdd" @close="showAdd=false" @confirmed="handleAdd" />
  </div>
</template>

<script>
import axios from 'axios'
import { ref, onMounted } from 'vue'
import ApproveModal from './components/ApproveModal.vue'
import RejectModal from './components/RejectModal.vue'
import AddModal from './components/AddModal.vue'

export default {
  components: { ApproveModal, RejectModal, AddModal },
  setup() {
    const items = ref([])
    const selected = ref([])
    const showApprove = ref(false)
    const showReject = ref(false)
    const showAdd = ref(false)

    const load = async () => {
      const r = await axios.get('/api/items')
      items.value = r.data.items
      selected.value = selected.value.filter(id => items.value.some(it => it.id === id && it.status === 'pending'))
    }

    onMounted(load)

    const statusLabel = s => s === 'pending' ? 'รออนุมัติ' : s === 'approved' ? 'อนุมัติ' : 'ไม่อนุมัติ'

    const handleClose = () => {
      showApprove.value = false
      showReject.value = false
      showAdd.value = false
    }

    const handleApprove = async ({ ids, reason }) => {
      await axios.post('/api/items/approve', { ids, reason }, { headers: { 'Content-Type': 'application/json' }})
      await load()
      handleClose()
      selected.value = []
    }

    const handleReject = async ({ ids, reason }) => {
      await axios.post('/api/items/reject', { ids, reason }, { headers: { 'Content-Type': 'application/json' }})
      await load()
      handleClose()
      selected.value = []
    }

    const handleAdd = async ({ title }) => {
      await axios.post('/api/items/add', { title })
      await load()
      showAdd.value = false
    }

    return { items, selected, showApprove, showReject, showAdd, load,
             statusLabel, handleClose, handleApprove, handleReject, handleAdd }
  }
}
</script>