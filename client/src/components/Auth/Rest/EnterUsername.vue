<template>
  <div>
    <div v-if="this.nextComp == false">
      <i id="lock" class="fas fa-lock"></i>
      <br>
      <br>
      <div id="inputnewpass" class="field">
          <p class="control has-icons-left">
          <input class="input is-small" v-model.trim="usernamePassReset" @input="delayTouch($v.usernamePassReset)" type="text" placeholder="Username" @keyup.enter="GetQuestions" v-bind:class="{error: $v.usernamePassReset.$error, valid: $v.usernamePassReset.$dirty && !$v.usernamePassReset.$invalid}">
          <span class="icon is-small is-left">
          <i class="fa fa-user"></i>
          </span>
          </p>
          <div class="errorMessage">
            <span v-show="!$v.usernamePassReset.required && $v.usernamePassReset.$dirty">Username is required</span>
          </div>
      </div>
      <button class="button is-primary" @click="GetQuestions" :disabled="$v.$invalid">Reset Password</button>
      <p v-if="noUsers" class="help is-danger">Invalid Username</p>
    </div>
    <div>
      <AnswerQuestions v-if="nextComp == true" :Questions="sentQuestions"></AnswerQuestions>
    </div>
  </div>
</template>
<script>
import axios from 'axios'
import AnswerQuestions from '@/components/Auth/Rest/AnswerQuestions'
import {required} from 'vuelidate/lib/validators'
const touchMap = new WeakMap()
export default {
  name: 'EnterUsername',
  components: {
    'AnswerQuestions': AnswerQuestions
  },
  data () {
    return {
      usernamePassReset: '',
      noUsers: false,
      questions: null,
      nextComp: false,
      sentQuestions: {
        Username: '',
        incommingQs: null
      }
    }
  },
  validations: {
    usernamePassReset: {
      required
    }
  },
  methods: {
    delayTouch ($v) {
      $v.$reset()
      if (touchMap.has($v)) {
        clearTimeout(touchMap.get($v))
      }
      touchMap.set($v, setTimeout($v.$touch, 1000))
    },
    GetQuestions: function () {
      axios({
        method: 'POST',
        url: this.$store.getters.getURL + 'v1/ResetPassword/GetQuestions',
        headers: this.$store.getters.getheader,
        data: {
          Username: this.$data.usernamePassReset
        }
      })
        .then((response) => {
          console.log(response)
          this.$data.questions = response.data
          this.$data.sentQuestions.Username = this.$data.usernamePassReset
          this.$data.sentQuestions.incommingQs = response.data.Questions
          this.$data.nextComp = true
        })
        .catch((error) => {
          if (error.response.status === 400) {
            // Your custom messages that appears on the screen
            this.$data.noUsers = true
          } else if (error.response.status === 404) {
            // Redirects you to the 404 page
            this.$data.noUsers = true
            // this.$router.push('/notfound')
          } else if (error.response.status === 403) {
            // Redirects you to the Forbidden page
            this.$data.noUsers = true
            this.$router.push('/notAllowed')
          } else if (error.response.status === 500) {
            // Redirects you to the server issue page
            this.$data.noUsers = true
            this.$router.push('/serverissue')
          }
          this.$data.usernamePassReset = ''
          this.$data.noUsers = true
        })
    }
  }
}
</script>

<style scoped>
#lock {
  font-size: 7em;
  align-items: center;
}
#inputnewpass {
  padding-left: 10%;
  padding-right: 10%;
}
</style>
