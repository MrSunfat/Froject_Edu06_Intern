<template>
  <div class="header d-flex">
    <div class="header__left d-flex">
      <h1 class="title">Cán bộ, giáo viên</h1>
      <div class="help-icon header__help-icon show-tooltip">
        <tooltip-comp :contentTooltip="this.titleHelp" />
      </div>
    </div>
    <div class="header__right d-flex">
      <div class="header__notify">
        <img
          src="../../assets/Icons/ic_Notification_32.png"
          alt="notify icon"
          class="notify-icon"
        />
        <div class="dot-on-bell"></div>
      </div>
      <div class="party-wall"></div>
      <div class="header__user d-flex">
        <div class="header__user-name d-flex">
          <h1 class="title">{{ userName?.name }}</h1>
          <div class="down-icon header__down-icon"></div>
        </div>
        <div class="avatar">
          <img
            src="../../assets/Icons/ic_Avatar_36.png"
            alt=""
            class="avatar-icon"
          />
        </div>
        <div class="info-and-option-user">
          <div class="avatar">
            <img
              src="../../assets/Icons/ic_Avatar_36.png"
              alt=""
              class="avatar-icon"
            />
          </div>
          <div class="info__user d-flex">
            <h1 class="title info__user__name">{{ userName?.name }}</h1>
            <span class="small-text info__user__position">{{
              formatPosition
            }}</span>
          </div>
          <div class="seperate"></div>
          <div class="logout-account d-flex">
            <div class="logout-icon">
              <svg
                id="Layer_1"
                data-name="Layer 1"
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 32 32"
              >
                <g id="Group_33236" data-name="Group 33236">
                  <circle
                    id="Ellipse_5591"
                    data-name="Ellipse 5591"
                    cx="16"
                    cy="16"
                    r="16"
                    style="fill: #e5e6eb"
                  ></circle>
                  <g id="enter">
                    <path
                      id="Path_63134"
                      data-name="Path 63134"
                      d="M12.79,16.12H5.57A.59.59,0,0,1,5,15.54.57.57,0,0,1,5.57,15h7.22a.57.57,0,0,1,.57.57A.58.58,0,0,1,12.79,16.12Z"
                      style="fill: #5f656f"
                    ></path>
                    <path
                      id="Path_63135"
                      data-name="Path 63135"
                      d="M9.94,19a.57.57,0,0,1-.57-.58.56.56,0,0,1,.17-.4L12,15.58,9.54,13.13a.57.57,0,0,1,.8-.81l2.85,2.85a.58.58,0,0,1,0,.81l-2.85,2.85A.54.54,0,0,1,9.94,19Z"
                      style="fill: #5f656f"
                    ></path>
                    <path
                      id="Path_63136"
                      data-name="Path 63136"
                      d="M17.29,25.24a1.56,1.56,0,0,1-1.65-1.44V10.87a1.47,1.47,0,0,1,1.12-1.36l5-1.44a1.65,1.65,0,0,1,2.1,1,1.35,1.35,0,0,1,.08.36V22.36a1.47,1.47,0,0,1-1.12,1.37l-5,1.43a1.58,1.58,0,0,1-.53.07Zm5-16.16a.39.39,0,0,0-.15,0l-4.95,1.43a.37.37,0,0,0-.28.34V23.8a.43.43,0,0,0,.56.34l4.95-1.43a.38.38,0,0,0,.28-.35V9.44a.38.38,0,0,0-.41-.36Z"
                      style="fill: #5f656f"
                    ></path>
                    <path
                      id="Path_63137"
                      data-name="Path 63137"
                      d="M11.65,11.8a.57.57,0,0,1-.57-.57h0V10.09A2.09,2.09,0,0,1,13.17,8h8.55a.57.57,0,0,1,0,1.14H13.17a1,1,0,0,0-.95.95v1.14A.58.58,0,0,1,11.65,11.8Z"
                      style="fill: #5f656f"
                    ></path>
                    <path
                      id="Path_63138"
                      data-name="Path 63138"
                      d="M16.21,23h-3a2.1,2.1,0,0,1-2.09-2.09V19.73a.58.58,0,0,1,.57-.58.57.57,0,0,1,.57.57h0v1.14a1,1,0,0,0,.95.95h3a.56.56,0,0,1,.57.57.57.57,0,0,1-.57.57Z"
                      style="fill: #5f656f"
                    ></path>
                  </g>
                </g>
              </svg>
            </div>
            <h4 class="subtitle-two">Đăng xuất</h4>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import TooltipComp from "../common/tooltip/TooltipComp.vue";
export default {
  components: { TooltipComp },
  name: "TheHeader",
  data() {
    return {
      titleHelp: "Trợ giúp",
    };
  },
  props: {
    userName: Object,
  },
  computed: {
    formatPosition() {
      return `${this.userName?.position} (${this.userName?.name})`;
    },
  },
};
</script>

<style scoped>
.header {
  /* background-color: #000; */
  justify-content: space-between;
  padding: 5px 16px;
  box-shadow: 0 0 5px #b1b1b1;
}

/* header left */
.header__left {
  cursor: pointer;
}

.header__left > h1.title {
  color: var(--titlle-header-left-color);
  cursor: text;
}

.help-icon {
  width: 24px;
  height: 24px;
  background-image: url("../../assets/Icons/ic_sprites.svg");
  background-repeat: no-repeat;
  background-position: -804px -295px;
  cursor: pointer;
}

.header__help-icon {
  margin-left: 8px;
}

/* header right */
.header__right {
  cursor: pointer;
}

.header__notify {
  position: relative;
}

.dot-on-bell {
  position: absolute;
  content: "";
  border-radius: 50%;
  background-color: var(--dot-on-bell);
  width: 10px;
  height: 10px;
  top: 6px;
  left: 20px;
  border: 1px solid #fff;
}

.dot-on-bell.check {
  display: none;
}

.party-wall {
  background-color: var(--bg-party-wall);
  width: 1px;
  height: 28px;
  /* background-color: #000; */
  margin-left: 10px;
  margin-right: 16px;
}

.header__user-name:hover h1 {
  text-decoration: underline;
}

.header__user {
  position: relative;
}

.header__user-name {
}

.down-icon {
  background: url("../../assets/Icons/ic_sprites.svg") no-repeat -185px -165px;
  width: 11px;
  height: 7px;
}

.header__down-icon {
  margin-left: 8px;
  margin-right: 8px;
}

.header__user > .avatar {
  height: 40px;
  width: 40px;
}

.header__user > .avatar > .avatar-icon {
  width: 100%;
  height: 100%;
}

/* user option */
.header__user:hover .info-and-option-user {
  display: block;
}

.info-and-option-user {
  position: absolute;
  content: "";
  flex-direction: column;
  top: 48px;
  right: 0px;
  padding-top: 18px;
  /* height: 256px; */
  background-color: #fff;
  box-shadow: 0 6px 12px rgb(0 0 0 / 18%);
  border-radius: 0.25rem;
  width: var(--width-user-option);
  display: none;
  cursor: default;
  z-index: 2;
  /* overflow: hidden; */
}

.info-and-option-user::after {
  position: absolute;
  content: "";
  top: -8px;
  right: 75px;
  width: 0;
  height: 0;
  border-left: 8px solid transparent;
  border-right: 8px solid transparent;
  border-bottom: 8px solid #fff;
}

.info-and-option-user::before {
  position: absolute;
  content: "";
  top: -24px;
  right: 0;
  width: 100%;
  height: 23px;
  background-color: transparent;
}

.info-and-option-user > .avatar {
  height: 68px;
  width: 68px;
  margin: 0 auto 20px;
}

.info-and-option-user > .avatar > .avatar-icon {
  height: 100%;
  width: 100%;
}

.info__user {
  flex-direction: column;
}

.info__user__position {
  color: var(--user-position-color);
  margin-top: 3px;
  margin-bottom: 16px;
}

.seperate {
  background-color: #f2f4f4;
  height: 1px;
  margin: 0 16px;
}

.logout-account {
  padding: 15px 26px 12px 36px;
  cursor: pointer;
}

.logout-account:hover {
  background-color: var(--option-user-color);
}

.logout-icon {
  width: 35px;
  height: 35px;
  margin-right: 16px;
}
</style>