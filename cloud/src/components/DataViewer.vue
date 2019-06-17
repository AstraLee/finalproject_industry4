<script>
import { Line } from '../BaseChart'
import { setTimeout, setInterval } from 'timers';

export default {
  extends: Line,
  data() {
      return {
          data: [],
          labels: [],
          label: [],
      }
  },
  async mounted () {
    await this.fetchData();
    /*
    setInterval(() => {
        console.log(this.labels)
        this.renderChart({
        labels: this.labels,
        datasets: [
            {
            label: this.label,
            backgroundColor: 'rgba(26, 249, 242, 0.52)',
            data: this.data
            },
        ]
        }, {responsive: true, maintainAspectRatio: false})
   }, 1000);
   */
    setTimeout(() => {
        console.log(this.labels)
        this.renderChart({
        labels: this.labels,
        datasets: [
            {
            label: this.label,
            backgroundColor: 'rgba(26, 249, 242, 0.52)',
            data: this.data
            },
        ]
        }, {responsive: true, maintainAspectRatio: false})
    }, 500);
  },
  methods: {
      fetchData() {
          const url = "ws://127.0.0.1:8081";
          this.socket = new WebSocket(url);
          this.socket.onopen = this.websocketopen;
          this.socket.onmessage = this.websocketonmessage;
          this.socket.onerror = this.websocketonerror;
          this.socket.onclose = this.websocketclose;
      },
      websocketopen() {
          console.log("open");
      },
      websocketonerror() {
          console.log("error");
      },
      websocketonmessage(e) {
          const data = JSON.parse(e.data);
          this.data = data.data.split(",")
          this.labels = [...Array(this.data.length).keys()]
          this.label = data.label
      },
      websocketclose() {
          console.log("disconnect");
      },
  },
}
</script>